const colorDatabase = [
    { name: "Midnight Navy", hex: "#0f172a" },
    { name: "Slate Grey", hex: "#64748b" },
    { name: "Soft Rose", hex: "#fb7185" },
    { name: "Emerald Mint", hex: "#10b981" },
    { name: "Cyber Purple", hex: "#a855f7" },
    { name: "Sunset Orange", hex: "#f97316" },
    { name: "Ocean Blue", hex: "#0ea5e9" },
    { name: "Eco Green", hex: "#22c55e" },
    { name: "Lemon Zinc", hex: "#eab308" },
    { name: "Ruby Red", hex: "#e11d48" },
    { name: "Deep Forest", hex: "#064e3b" },
    { name: "Cloud White", hex: "#f8fafc" },
    { name: "Velvet Plum", hex: "#581c87" },
    { name: "Electric Indigo", hex: "#6366f1" },
    { name: "Golden Sand", hex: "#f59e0b" },
    { name: "Sky Glaze", hex: "#bae6fd" },
    { name: "Dark Carbon", hex: "#171717" },
    { name: "Terracotta", hex: "#c2410c" },
    { name: "Lavender", hex: "#d8b4fe" },
    { name: "Coffee", hex: "#451a03" },
    { name: "Cherry Blossom", hex: "#F9A8BB" },
    { name: "Deep Twilight", hex: "#1A1265" },
    { name: "Celadon", hex: "#A8D3A8" },
    { name: "Chocolate Plum", hex: "#553832" },
    { name: "Lime Cream", hex: "#DDEA78" },
    { name: "Vintage Grape", hex: "#433455" },
    { name: "Electric Rose", hex: "#FE00AE" },
    { name: "Chartreuse", hex: "#C1FE1A" },
    { name: "Shadow Grey", hex: "#272727" },
    { name: "Sandy Clay", hex: "#D4AA7D" },
    { name: "Raspberry Red", hex: "#EE005A" },
    { name: "Deep Space Blue", hex: "#012641" },
    { name: "Icy Blue", hex: "#A4D8FF" },
    { name: "Gunmetal", hex: "#35393C" }
    // ... Je kunt deze lijst zelf aanvullen tot 50+ kleuren
];

// Automatisch aanvullen tot 50 voor demo-doeleinden
for(let i=1; i<=30; i++) {
    colorDatabase.push({ 
        name: `Variatie ${i}`, 
        hex: `#${Math.floor(Math.random()*16777215).toString(16).padStart(6, '0')}` 
    });
}

const libraryContainer = document.getElementById('colorLibrary');
const picker = document.getElementById('colorPicker');
const nameDisplay = document.getElementById('colorNameDisplay');
const hexText = document.getElementById('hexText');

// Bouw de lijst
colorDatabase.forEach(item => {
    const div = document.createElement('div');
    div.className = 'color-item';
    div.innerHTML = `
        <div class="swatch" style="background-color: ${item.hex}"></div>
        <span class="name-label">${item.name}</span>
        <span class="hex-label">${item.hex.toUpperCase()}</span>
    `;
    
    div.onclick = () => {
        picker.value = item.hex;
        updateUI(item.hex, item.name);
    };
    
    libraryContainer.appendChild(div);
});

picker.addEventListener('input', (e) => {
    updateUI(e.target.value, "Custom Kleur");
});

function updateUI(hex, name) {
    hexText.textContent = hex.toUpperCase();
    nameDisplay.textContent = name;
    document.body.style.backgroundColor = hex + "0";
}

function copyToClipboard() {
    navigator.clipboard.writeText(hexText.textContent).then(() => {
        const status = document.getElementById('copyStatus');
        status.style.display = 'inline';
        setTimeout(() => status.style.display = 'none', 1500);
    });
}