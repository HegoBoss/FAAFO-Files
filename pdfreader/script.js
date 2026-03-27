// We vertellen PDF.js waar de 'worker' staat (nodig voor prestaties)
pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/3.4.120/pdf.worker.min.js';

let pdfDoc = null,
    pageNum = 1,
    pageIsRendering = false,
    pageNumPending = null;

const scale = 1.5,
    canvas = document.querySelector('#pdf-render'),
    ctx = canvas.getContext('2d');

// Functie: Render de pagina
const renderPage = num => {
    pageIsRendering = true;

    // Haal pagina op
    pdfDoc.getPage(num).then(page => {
        const viewport = page.getViewport({ scale });
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        const renderCtx = {
            canvasContext: ctx,
            viewport
        };

        page.render(renderCtx).promise.then(() => {
            pageIsRendering = false;

            if (pageNumPending !== null) {
                renderPage(pageNumPending);
                pageNumPending = null;
            }
        });

        // Update pagina nummers in de HTML
        document.querySelector('#page-num').textContent = num;
    });
};

// Functie: Controleer of er een volgende/vorige pagina is
const queueRenderPage = num => {
    if (pageIsRendering) {
        pageNumPending = num;
    } else {
        renderPage(num);
    }
};

// Event: Bestand kiezen
document.querySelector('#file-selector').addEventListener('change', (e) => {
    const file = e.target.files[0];
    if (file.type !== 'application/pdf') {
        alert("Selecteer aúb een PDF bestand.");
        return;
    }

    const reader = new FileReader();
    reader.onload = function() {
        const typedarray = new Uint8Array(this.result);

        // Laad de PDF data
        pdfjsLib.getDocument(typedarray).promise.then(pdfDoc_ => {
            pdfDoc = pdfDoc_;
            document.querySelector('#page-count').textContent = pdfDoc.numPages;
            
            renderPage(pageNum);
        });
    };
    reader.readAsArrayBuffer(file);
});

// Knoppen logica
document.querySelector('#prev-page').addEventListener('click', () => {
    if (pageNum <= 1) return;
    pageNum--;
    queueRenderPage(pageNum);
});

document.querySelector('#next-page').addEventListener('click', () => {
    if (pageNum >= pdfDoc.numPages) return;
    pageNum++;
    queueRenderPage(pageNum);
});