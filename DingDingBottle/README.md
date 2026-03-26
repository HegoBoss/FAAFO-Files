[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/hVK-jj9o)
# PE01-Web Frontend Advanced - Numbers
## Algemeen
Maak een applicatie waarbij je vanaf de indexpagina kan navigeren naar een spelpagina. Voorzie deze indexpagina van een eigen opmaak, met mogelijk speluitleg.

Je kan hier eventueel gebruik maken van de algemene template.

## Spel: DingDingBottle
Maak een applicatie waarbij je een speelveld vult met exact 30 vakjes. Deze vakjes bevatten getallen die random gekozen worden tussen 1 en 101. De applicatie geeft via een duidelijke weergave weer hoeveel speciale nummers er in het speelveld zitten.

### Definities :

| Definitie getal | Beschrijving |
| :--------- | --------:|
| Ding | Getal enkel deelbaar door 2|
|Bottle | Getal enkel deelbaar door 3|
|DingDingBottle | Getal deelbaar door 2 én 3 |
|Nummer | Getal die niet voldoet aan bovenstaande |



### Functionaliteiten
Wanneer een gebruiker een vak aanklikt, wordt weergegeven aan de hand van opmaak welk soort getal dit is:

- Indien Ding-getal voorzie een gele achtergrond met een blauwe rand.
- Indien Botle-getal voorzie een oranje achtergrond met blauwe rand.
- Indien DingDingBottle-getal voorzie een groene achtergrond met een blauwe rand. 
- Indien Nummer-getal voorzie een licht-rode achtergrond met een rode rand 

De gebruiker kan volgen of welke getallen al zijn geselecteerd, dit omdat de weergave van de soorten wordt aangepast wanneer er een speciaal nummer is geselecteerd.


Zijn alle DingDingBottle-getallen geraden, krijgt de gebruiker een melding en kan deze een nieuw spel opstarten. (Voorzie hiervoor dynamisch een knop - die een reset toelaat)



#### Aandacht:

- Zorg dat de gebruiker duidelijk ziet welk soort getal er werd aangeklikt (visuele weergave bij element)
- Let erop dat reeds aange(klikte)duide getallen niet meer aanklikbaar zijn.
- Zorg ervoor dat bij het speleinde (alle DingDongBottle-getallen gevonden) niet verder kan geselecteerd worden.
- Om het spel te resetten gebruik je **geen** `window.location.reload()`, voorzie hiervoor een separate functie.

## Tip
Om een random getal te genereren kan je gebruik maken van een van volgende statements :
```js
// Genereert een random getal van 0(inbegrepen) tot 99(inbegrepen)
// waarin Math.random() en floating-point getal oplevert tussen 0 en 1
const random = Math.floor(Math.random() * 100);

// Genereert een random getal van 1 tot en met 100
const randomNumber = Math.ceil(Math.random() * 100);

```
Geef de pagina's een duidelijke naam en link deze met de css en js door dezelfde naam te gebruiken voor deze pagina's.

# Gebruik AI-tools
Voor deze opdracht mogen geen AI-tools gebruikt worden.

![Gebruik AI](screens/AI_Nietgebruiken.png)