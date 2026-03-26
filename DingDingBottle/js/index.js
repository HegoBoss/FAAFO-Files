/**
 * index.js - Logica voor de startpagina
 */

// Wacht tot de volledige HTML geladen is voordat we scripts uitvoeren
document.addEventListener('DOMContentLoaded', () => {
    console.log("Welkom bij DingDingBottle! Veel succes met het spel.");

    // Optioneel: Je kunt hier een animatie toevoegen aan de startknop
    const startKnop = document.querySelector('.start-knop');
    
    if (startKnop) {
        startKnop.addEventListener('mouseover', () => {
            startKnop.style.transform = 'scale(1.1)';
            startKnop.style.transition = '0.3s';
        });

        startKnop.addEventListener('mouseout', () => {
            startKnop.style.transform = 'scale(1)';
        });
    }
});