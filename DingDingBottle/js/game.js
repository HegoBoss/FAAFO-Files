// Variabelen bovenaan je script
let dingDingBottlesToFind = 0;
let dingDingBottlesFound = 0;

function setupGame() {
    const grid = document.getElementById('grid');
    const controls = document.getElementById('controls');
    const boodschap = document.getElementById('boodschap');

    // 1. Maak de controls container LEEG (dit verwijdert de 'Nieuw Spel' knop)
    controls.innerHTML = '';
    
    // 2. Reset de tekst en variabelen
    grid.innerHTML = ''; 
    boodschap.innerText = "Zoek de speciale nummers!";
    dingDingBottlesToFind = 0;
    dingDingBottlesFound = 0;

    // 3. Genereer 30 vakjes tussen 1 en 101
    for (let i = 0; i < 30; i++) {
        const randomNumber = Math.floor(Math.random() * 101) + 1; //
        const tile = document.createElement('div');
        tile.classList.add('tile');
        tile.innerText = randomNumber;

        // Tel de DingDingBottles (deelbaar door 2 EN 3)
        if (randomNumber % 2 === 0 && randomNumber % 3 === 0) {
            dingDingBottlesToFind++;
        }

        tile.addEventListener('click', function() {
            checkNumber(tile, randomNumber);
        });

        grid.appendChild(tile);
    }
}

function checkNumber(element, num) {
    // Logica voor de kleuren op basis van deelbaarheid
    if (num % 6 === 0) {
        element.classList.add('dingdingbottle');
        dingDingBottlesFound++;
    } else if (num % 2 === 0) {
        element.classList.add('ding');
    } else if (num % 3 === 0) {
        element.classList.add('bottle');
    } else {
        element.classList.add('nummer');
    }

    element.classList.add('disabled'); // Maak het vakje niet meer aanklikbaar
    
    // Controleer of het spel klaar is
    if (dingDingBottlesFound === dingDingBottlesToFind && dingDingBottlesToFind > 0) {
        endGame();
    }
}

function endGame() {
    const controls = document.getElementById('controls');
    const boodschap = document.getElementById('boodschap');
    
    boodschap.innerText = "Gefeliciteerd! Alle DingDingBottles gevonden.";
    
    // Voorkom dat er nog verder geklikt kan worden
    document.querySelectorAll('.tile').forEach(t => t.classList.add('disabled'));

    schietVuurwerkAf();

    // Maak de reset-knop aan
    const resetBtn = document.createElement('button');
    resetBtn.innerText = "Nieuw Spel";
    resetBtn.className = "reset-knop";
    
    // Wanneer je klikt, start setupGame opnieuw en de knop verdwijnt door controls.innerHTML = ''
    resetBtn.onclick = setupGame; 
    
    controls.appendChild(resetBtn);
}

function schietVuurwerkAf() {
    // We maken 50 deeltjes aan voor een mooie explosie
    for (let i = 0; i < 50; i++) {
        const deeltje = document.createElement('div');
        deeltje.classList.add('vuurwerk-deeltje');

        // Geef elk deeltje een willekeurige felle kleur
        const willekeurigeKleur = `hsl(${Math.random() * 360}, 100%, 50%)`;
        deeltje.style.backgroundColor = willekeurigeKleur;

        // Bepaal een willekeurige richting (tx = X-as, ty = Y-as)
        const xRichting = (Math.random() - 0.5) * 600; // Tussen -300px en +300px
        const yRichting = (Math.random() - 0.5) * 600;

        // Geef de willekeurige richting door aan onze CSS animatie
        deeltje.style.setProperty('--tx', `${xRichting}px`);
        deeltje.style.setProperty('--ty', `${yRichting}px`);

        // Zet het deeltje op het scherm
        document.body.appendChild(deeltje);

        // Ruim het deeltje op na de animatie (1 seconde = 1000 milliseconden)
        setTimeout(() => {
            deeltje.remove();
        }, 1000);
    }
}

// Start het eerste spel direct bij het laden van de pagina
document.addEventListener('DOMContentLoaded', setupGame);