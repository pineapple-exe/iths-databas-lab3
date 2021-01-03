#Bakgrund

Tomtens ideella verksamhet har rötter som sträcker sig långt bak i tiden, djupt in i människans sägner. Under medeltiden såg man Sankt Nikolaus med en djävulsliknande statur hack i häl. Det diaboliska är inget främmande för eller farligt enligt Tomten, därför har han förmågan att hålla det onda i schack. I praktiken innebär det att verksamheten inte nöjer sig med det meritokratiska systemet, där det goda belönas och det onda bestraffas. Att värna om alla barn innebär att sträva efter balans och kunna utöva nåd. Och så utvecklades pepparkaksreceptet, för att skänka godhet till de som berövats det. Pepparkakor gör oss snälla, sägs det, men ingen vet hur. Processerna för moralisk fingertoppskänsla samt produktion och utdelning av julklappar har fått en mystisk aura genom sin effektivitet. Med digitalisering kan verksamheten fortsätta utvecklas med den växande samvaron.


#Processbeskrivning

Tomtenissarna knappar in barndata för de barn som tillhör åldersgruppen som verksameten har kommit överens om att tjäna.

När julklappar har nått lagret knappas dess data in.

Beteende för respektive barn innevarande år knappas i regel in så snart som möjligt efter september månad.

Julklappar och barn paras ihop via "Match presents with children"-funktionen, allteftersom beteende matas in och lagret fylls på med klappar. Detta hålls tight för att styra julklappsproduktionen. Julklapparna slås därefter in, markerade med namn och address.

Pepparkaksdistribuering sker baserat på de tre föregångna åren, under juletid innevarande år, inför nästkommande år (på så vis kan man se om ett barns beteende har korrigerats före eller efter intervention, vilket fortsättningsvis kan användas i samband med förfiningen av pepparkaksreceptet).

*Och massa magi däremellan!*


#Användning

Om du vill skapa en egen databas så kan du konfigurera den i connection string i SaintNicholasDbContext.cs
Sen är det bara att köra Update-Database i Package Manager Console.

Vid uppstart kommer du att få välja att lägga in testdata (du vill det).

Under menyval "Children" kan Tomten
1. lägga till ett barn i databasen (och därmed fylla i samtliga data för detta barn), 
2. ändra redan existerande data selektivt för ett barn baserat på dess Id, 
3. baserat på Id ta bort ett barn från databasen,
4. visa all barndata.
Gender kan sättas till "girl", "boy" men även "u" som kan användas för att representera allmänt ospecificerat eller aktivt särskilt fall.

Under menyval "Christmas presents" kan Tomten
1. lägga till en eller flera julklappar av en typ (och därmed fylla i samtliga data för denna batch),
2. kolla hur många barn som innevarande år inte har matchats med en julklapp, samt om de saknade klapparna är roliga / tråkiga / obestämt, samt könsneutrala / för flickor / för pojkar,
3. använda match-algoritmen som parar ihop existerande julklappar med barn vars beteenden har registrerats i Behavioral records. Detta görs för innevarande år, med kön och beteende i åtanke,
4. visa all julklappsdata.

Under menyval "Children's behavior" kan Tomten
1. sätta status (naughty, y/n) på ett barn (baserat på barnets Id, innevarande år),
2. kolla vilka barn som har varit naughty varje år de tre föregående åren,
3. kolla vilka barn som har varit inte-naughty varje år de tre föregående åren,
4. visa all beteendedata.

I menyläget navigerar man sig med piltangenterna, Enter och Backspace.