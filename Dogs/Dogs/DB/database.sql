CREATE DATABASE `dogapp` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;

CREATE TABLE `dogapp`.`users` 
(`user_id` INT NOT NULL AUTO_INCREMENT , 
`username` VARCHAR(16) NOT NULL , 
`password` VARCHAR(200) NOT NULL , 
`salt` VARCHAR(200) NOT NULL,
PRIMARY KEY (`user_id`)) ENGINE = InnoDB;

CREATE TABLE `dogapp`.`points` 
(`id` INT NOT NULL AUTO_INCREMENT , 
`user_id` INT NOT NULL , 
`points` INT NOT NULL , 
PRIMARY KEY (`id`),
FOREIGN KEY (`user_id`) REFERENCES users(`user_id`)
) ENGINE = InnoDB;

CREATE TABLE `dogapp`.`images` 
(`id` INT NOT NULL AUTO_INCREMENT , 
`user_id` INT NOT NULL , 
`bought_images` VARCHAR(200) NOT NULL , 
PRIMARY KEY (`id`),
FOREIGN KEY (`user_id`) REFERENCES users(`user_id`)
) ENGINE = InnoDB;

CREATE TABLE `dogapp`.`dogs` 
(`dog_id` INT NOT NULL AUTO_INCREMENT , 
`dog_name` VARCHAR(40) NOT NULL ,
PRIMARY KEY (`dog_id`)) ENGINE = InnoDB;

INSERT INTO `dogapp`.`dogs` (`dog_name`)
VALUES
('rottweiler'),('németjuhász'),('francia bulldog'),('komondor'),
('kuvasz'),('mudi'),('erdélyi kopó'),('puli'),('pumi'),('agár'),('vizsla');

CREATE TABLE `dogapp`.`notes` 
(`id` INT NOT NULL AUTO_INCREMENT , 
`dog_id` INT NOT NULL , 
`notes` VARCHAR(500) NOT NULL , 
PRIMARY KEY (`id`),
FOREIGN KEY (`dog_id`) REFERENCES dogs(`dog_id`)
) ENGINE = InnoDB;

INSERT INTO `dogapp`.`notes` (`dog_id`,`notes`)
VALUES
('1','Felelősségteljes és tapasztalt kezekben idegerős, munkaszerető és hűséges kutya, aki kiválóan alkalmas családi kedvencnek is. Gyerekekkel szemben gyengéd, és más álatokkal is jól kijön.'),
('1','Szereti a játékot és tombolást a szabadban, valamint élvezi a kreatív sportprogramokat, melyek során kifáraszthatja magát. Megfelelő mennyiségű mozgás esetén higgadt, és könnyen vezethető eb.'),
('1','Védelmi ösztöne, bátor jelleme és testi ereje miatt nagyszerű házőrző. Engedelmes, ami ideális szolgálati-, munka- és kísérőkutyává teszi.'),
('1','Izmos teste és erőteljes, széles állkapcsa tiszteletet parancsol. Zömök és súlyos termete energiától duzzad. A kanok kb. 50 kg-ot nyomnak, a szukák valamivel könnyebbek, a 42 kg-ukkal.'),
('1','A rómaiak terelő- és pásztorkutyaként tartották a mai rottweiler őseit. Így a világ egyik legrégebbi kutyafajtájának számít. Napjainkban legfőképp szolgálati kutyaként vannak jelen.'),
('1','Tapasztalt, magabiztos kutyatartónak kell lenni ahhoz, hogy az első 9 hónapban már az eb megtanulja, hogyan fékezze erejét és hogyan kövesse gazdija utasításait.'),
('1','A helytelen nevelés veszélyes magatartást válthat ki belőlük. Ennek elkerülésére egyes országokban szerepel a veszélyes kutyafajták listáján, ami azt jelenti, hogy tartása bizonyos feltételekhez kötött.'),
('1','Ápolása nem sok időráfordítással jár. Rövid szőrét elég csak ritkán megkefélni és a szőrváltás időszakában igényel csak rendszeres masszázst szőrtelenítő kesztyűvel.'),
('1','Azon embereknek ajánlott a tartása, akiknek elegendő idejük van kutyájuk helyes nevelésére,rendszeres testmozgásban tartására. Cserébe megbízható, barátságos és gyengéd barátja lesz az embernek.'),

('2','Bátor, kitartó, megbízható kutya. Ösztönei, terhelhetősége, magabiztossága legendás. Magas intelligenciájának, tanulási képességeinek köszönhetően szinte minden munkára alkalmas.'),
('2','Eredetileg pásztorkutyának tenyésztették, manapság legtöbbször a rendőrségnél, a hadseregben alkalmazzák, mint őrző-védőkutyát. Mentőkutyaként, terápiás kutyaként, vakvezetőként, lavinamentő kutyaként is dolgozik.'),
('2','Nagyszerű partner és a családi életbe is tökéletesen beleillik. Nagyon gyerekbarát állatok és más háziállatokkal is jól kijönnek. Mozgásignye nagy, sportos családi kutyának számít.'),
('2','Erős és izmos, de atlétikus és mozgékony. Középméretű kutyákhoz tartoznak. Előre forduló fülei és az enyhén ferdén elhelyezkedő szemei állandóan éber, figyelő külsőt kölcsönöznek neki.'),
('2','A németjuhász ősei valószínűleg már a 7. században Németországban éltek. A bárányok őrzése és terelése volt a fő feladatuk.'),
('2','Az egészséges fejlődéséhez fehérjék, ásványi anyagok és a vitaminok szükségesek. A legfontosabb számára a hús, amely ételének 70%-át teszi ki, 30%-a pedig zöldség, gyümölcs.'),
('2','Mivel nagyon népszerű kutya, rengetegen tenyésztik, így hajlamosabb betegségekre. Csípő és gerincproblémák, porcproblémák, szembetegségek és allergiás reakciók is gyakoriak náluk.'),
('2','A német juhász duplarétegű szőrzetét elég könnyű ápolni, feltéve ha rendszeresen megkeféljük. Figyelemmel kell tartani a kutya füleit, szemét és karmait. Időnként ezeknek is szüksége van tisztításra.'),
('2','Az a gazi, aki elég időt szán kutyája képzésére, fittségére és megfelelő etetésére, egy életrevaló, sportos, hű családi kutyát kap cserébe.'),

('3','Jókedvű, nagyon szimpatikus kistestű kutyus, aki sármjával és hízelgő természetével minden kutyabarátot gyorsan a mancsa köré csavar.'),
('3','Okosak, barátságosak, kedvelik a szeretgetéssel töltött órákat. Rendszerint jól kijönnek a gyermekekkel és a többi négylábúval, játékosak és figyelnek társaikra.'),
('3','Rövid, zömök testfelépítésével egy tömzsi négylábú benyomását kelti. Az állat súlya 8-14 kg közé szokott esni, de hajlamosak az elhízásra, erre szükséges figyelni.'),
('3','Kifejezett tenyésztése csak a 19. század második felében kezdődött el. A kutyaszakértők nem értenek egyet abban, hogy melyik fajtól származik, de úgy gondolják, hogy a kisnövésű angol bulldog az.'),
('3','Nevelése könnyű, magabiztos irányítás esetén. A fiatal francia bulldogok figyelme rövid ideig tartható fenn, ezért pozitív megerősítéssel, és pár-percből álló utasításokkal nevelhetőek a legjobban.'),
('3','Kis fejük miatt hajlamosak légzésproblémára, horkolásra. Rosszul viselik a hőséget, szembetegségre is hajlamosak, amelyet szükséges műtéti úton is korrigálni.'),
('3','A kutyák többségének az arcán a bőr redőkben ráncolódik. A gyulladás elkerülése érdekében nedves törlőkendővel rendszeresen ki kell tisztítani őket. Füleit is szükséges ellenőrizni.'),
('3','Bundája a legváltozatosabb színekben fordul elő a feketétől, fehértől kezdődően egészen a bézs, krém vagy sötét foltos színekig. Rövid bundájuk kevés időt igényel, könnyen ápolható.'),
('3','A francia bulldog passzol minden olyan kutyabarát emberhez, aki négylábújával nem sportolni szeretne. Megfelelő lehet családok és egyedülállók számára egyaránt és kiváló választás kezdő kutyatartók számára is.'),

('4','A magyarok a kutyák királyának nevezik, mert nemesség, erő, és nyugodt fölény árad belőle. Rendíthetetlenül bátor, bizalmatlan alaptermészetű, figyelmes, őrző-védő állat.'),
('4','A honfoglalók, a török besenyők, vagy a kun népek egyike hozhatta be a Kárpát-medencébe. Maga a komondor szó is török vagy kun eredetű, jelentése kunokhoz-tartozó.'),
('4','Tiszteletet parancsoló személyisége, öblös hangja, robusztus, kicsit félelmetes termete tökéletessé teszi az évszázadok óta tartó feladatára, amely haszonállatok, vagyontárgyak és nyájak őrzése.'),
('4','Egy nagyon öntudatos, akaratos és megvesztegethetetlen állat. Ritkán barátkozik ismeretlen négylábú barátokkal, ezért nála különösen fontos a korai szocializáció más kutyákkal.'),
('4','Az “emberi falkájához” tartozó gyerekeket szereti és védelmezi. Leginkább gondozójukhoz kötődnek, de megfelelő nevelés mellett kiegyensúlyozott társ lesz, aki kiváló családi kutyaként is.'),
('4','Szeretik önállóan bejárni a területüket, amihez a legmegfelelőbb egy parasztgazdaság, nagy vidéki birtok, de legalább egy nagy méretű kert lehet. A természetben érzik jól magukat és nem mondhatóak városi kutyának.'),
('4','Szívesen elkíséri gazdáját hosszú sétákra, bár mozgásigénye nem kifejezetten nagy. Nem lelkesíti őket a kutyasport, elegendő számukra a nagy terület, és boldogok lesznek.'),
('4','Szőrzete hosszú, zsinóros, bozontos, gubancos, kellően kemény és egyszerre dús is. A két véglet nemkívánatos, azaz amikor teljesen kifésült vagy teljesen gondozatlan/ápolatlan a bundája.'),
('4','Tapasztalt kutyatartót igényel, aki kellő hozzáértéssel képes nevelni őt, és nem várja el tőle a feltétel nélküli engedelmességet.'),

('5','Erős felépítésű, nagy testű, fehér hullámos szőrű kutyafajta. Tetszetős külseje akárcsak a komondoré, nemességet, rendíthetetlenséget, bátorságot és vakmerőséget áraszt.'),
('5','Ez a juhászkutya is a honfoglaló magyarokkal került a Kárpát-medencébe. A fajta neve valószínüleg török eredetű, mely jelentése “védelmező”/“őrző”.'),
('5','A középkorban vadászatra, későbbi korokban az otthonok és nyájak őrzésére használták. Kiképezhető terelő- vagy mentőkutyává, de a legfontosabb számára a védlmezés.'),
('5','A II. világháború az állományukat vészesen lecsökkentette. Feltételezték, hogy pusztán 30 fajtatiszta kuvasz maradt ekkor Magyarországon. Az újratenyésztésüben a németek és hazánk is jeleskedett.'),
('5','Jellemző rá a hűség, szorosan kötődik gondozójához, családjához, gyerekek szeretetreméltó társa. Az idegen emberekkel, állatokkal szemben bizalmatlan, de megfelelő neveléssel kiegyensúlyozott kutyává válik.'),
('5','Izmosak, robosztusak, oda kell figyelni rájuk, mert hajlamosak az elhízásra is. Egy szuka 45-50 kg, egy kan akár 60 kg is lehet.'),
('5','Szereti a hosszú sétákat és jó kísérőnek bizonyul lovagláshoz. Tér és mozgásigénye nagy, viszont a mérete miatt nem való kutyasporthoz.'),
('5','Sűrű bundáját könnyű ápolni, de erősen hullajtja a szőrét. Már kölyökkorban érdemes hozzászoktatni a kefe használatához, hiszen az évente kettő vedlés idején naponta kell fésüli bundáját.'),
('5','Tapasztalt, elegendő hellyel rendelkező kutyatartót igényel, aki hozzáértő és szeretetteljes nevelés mellett egy hűséges és nagyon intelligens négylábút kap cserébe.'),

('6','Igazi egyéniség, aki egy magabiztos, vidám, csibész tekintetű középmeretű kutya. Okosak, kíváncsiak és nagyon tanulékonyak. Aktívak, becsületesek és megbízhatóak.'),
('6','A magyarországi kutya megjelenése vitatott, valószínűleg úgy alakulhatott ki, hogy az ősi puliba, később a pumiba német eredetű spicckutyák örökíthették át fajtatulajdonságaikat.'),
('6','Alapvetően terelőkutya, de vaddisznó-hajtóvadászatra is használják. Kitűnő őrző-védő, sport és kísérőkutya. Ezek mellett remek házőrző, és kedvelt házieb.'),
('6','Nagy leterheltségre van szüksége aktív jelleme miatt, ezért ha nem farmon él, és nem terelőkutyaként tartott, akkor sok időt kell szánni a lefoglalására.'),
('6','Mindig készen állnak új kalandokra, családjukhoz, és más négylábú társaikhoz ragaszkodó, és nagyon hűséges. Szeret időt tölteni a hozzátartozóival, ő egy érzékeny/kedves társ.'),
('6','Tanulékonysága ellenére hiányzik belőle a megfelelési vágy, ami azt jelenti, hogyha egy feladatnak nem látja értelmét, akkor ő azt abbahagyja. Ezért fontos, hogy gazdájára falkavezérként tekintsen.'),
('6','Szívesen elkíséri gazdáját futáskor, biciklikörre, vagy akár lovagláskor is. Nagy előnye, hogy alig van vadászösztöne, ezért gond nélkül szabadon engedhető minden sportos tevékenység során.'),
('6','Viszonylag rövid szőre könnyen ápolható, így lakásban is nehézség nélkül tartható. Vedlés idején azonban javasolt naponta fésűlni, hogy csökkentsük a szőr mennyiségét a lakásban.'),
('6','A kényelmes, gyalogolni nem szerető embereknek nem való társnak. Kiváló olyan gazdiknak, akik szeretik a természetet, és egy aktív, szeretetteljes, örökmozgó kutyára vágynak.'),

('7','Jóindulatú, bátor, kitartó, szenvedélyes vadász, aki képes akár 100 km-t is lefutni. Alapvetően nyugodt, kiegyensúlyozott, de határozott és temperamentumos is egyben.'),
('7','Kiváló a szaglása és tájékozódó képessége, amelyet vadász és hajtókutyaként rendkívül jól ki tud használni. Órákon át képes követni a vadat, futás közben is el tudja különíteni a szagokat.'),
('7','Vérében van, hogy az elejtett vadat nem hagyhatja ott, türelmesen vár, míg a gazdája meg nem érkezik. Kisebb termetű áldozatát csengő hanggal jelzi, viszont a termetesebbeket méllyebb ugatással.'),
('7','Ősi magyar fajtának tekintjük a kopót, aki a honfoglaláskor érkezett a Kárpát-medence területére. Ezek a kutyák a már itt élő ebekkel keveredtek, így alakult ki a pannon kopó, az erdélyi kopó közvetlen őse.'),
('7','Nagyon szeretetteljes, igazi védelmező, aki házőrzőnek is kitűnő. A gyerekekkel szemben kedves, társaságukban bolondos. Türelmes velük szemben, így családi kutyának is ideális.'),
('7','Idegenekkel szemben bizalmatlan, ám ha látja, hogy gazdájának nincs ellenvetése, elfogadja az új ismerőst. Könnyen alkalmazkodik környezetéhez, viszont lakásokban nem igazán érzi jól magát.'),
('7','Igényli a mozgást, a munkát, minden nap kell vele foglalkozni, ő nem elégszik meg a kanapén pihenéssel. Kiváló túratárs, de póráz nélküli sétáltatása nem ajánlott, hiszen nagy benne a vadászösztön.'),
('7','Rövid, sűrű, egyenes szálú, testhez simuló, fényes, tömött szőre van, amely kicsit durva tapintású. Alapszíne fekete, de szemeinél, lábainál cserszínű, világosbarna foltok találhatóak.'),
('7','Csak határozott, megfelelő idővel rendelkező gazdák számára javasolt. Viszont aki a bizalmát kiérdemli az ebnek, cserébe egy hűséges, védelmező és kiváló társat kap maga mellé.'),

('8','Dolgos, hangos, játékos, érzékeny, mozgékony, nyílt egyéniségű és tanulékony középméretű kutya. Életrevalóak, temperamentumosak és okosak, egy nagy adag önbizalommal kombinálva.'),
('8','A komondor és a puli kutya hasonlósága nem a véletlen műve, valószínűleg a puli ősei is 1000 évvel ezelőtt, a magyarokkal együtt érkeztek Ázsiából Magyarországra.'),
('8','Alapvetően juhász és pásztorkutyaként dolgoztak, napjainkban kutyasportokban jeleskednek, mozgékonyságuk miatt. Ezen okos pásztorkutyák terápiás- és keresőkutyának is megfelelnek.'),
('8','Fogékonyak és éberek, így nem sok gondot okoz a nevelésük. Minden elvégzett feladat után érdemes jutalmazni/dicsérni őket, hiszen nagyon élvezik, ha elismerik a munkájukat.'),
('8','Nagyon hűségesek és ragaszkodóak gazdájukhoz. Tipikus pásztorkutya: hűséges a falkavezérhez, de gyanakvó az idegenekkel szemben, viszont nem agresszív. Nagyon gyerekszerető, családbarát kutya.'),
('8','Nincsen hajlama semmilyen öröklődő betegségre, megfelelő táplálkozás esetén jó egészségnek örvend. Rendszeresen kell mérni a súlyát nehogy elhízzon/lefogyjon, hiszen a szőrétől nem látszik a testalkata.'),
('8','Imádja a hosszabb sétákat, az ajtó előtt toporogva várja a kalandokat. Szívesen tartózkodik kint, a megfelelő környzete az udvar vagy vidék, ahol kedvére rohangálhat.'),
('8','Szőrét nem szükséges fésülgetni vagy kefélni, de cserébe rendszeresen „csomózni” kell, hogy a jellegzetes zsinóros bundát létrehozzuk. A rasztái feketék, fehérek vagy őzbarna színűek lehetnek.'),
('8','Egy aktív, törődő, leginkább vidéki, vagy kertes házban élő gazdinak ideális. Gyerekimádó, hűséges, bolondos jellemű kutyus, aki sokáig él (akár 16 év) ezért családi kutyának is remek.'),

('9','Eleven, élénk vérmésékletű, temperamentumos, nagy munkakedvvel megáldott, vakmerően bátor, okos, de rámenős kutya. Elég hangos tud lenni, szereti magáravonni a figyelmet.'),
('9','A puli ősi kutyafajta, és a német/francia terrierjellegű pásztorkutyák keveredéséből alakult ki kb. a 17. században. A pumikat részben csak puli-variációnak tekintették, de 1920-ban elismerték önálló fajtaként.'),
('9','Alapvetően hajtó és terelő feladatokat látott el, pásztorok és gulyások nagyon szerették, mert feladatát hihetetlenül komolyan veszi, és szigorú rendet tart még marhacsordák esetén is.'),
('9','Rendkívül tanulékonynak számít, s következetességgel, valamint sok dicsérettel jól nevelhető, ha számításba vesszük, hogy, pásztor/terelőkutyaként önálló karaktere is jelen van.'),
('9','A pumik lojálisak a családjukhoz, emiatt kellőképpen távolságtartóak az idegenekkel szemben. Ez a fajta igen gyerekszeretőnek számít, védelmezi őket, és szeretet játszani velük.'),
('9','Igazán megfelelő életterérben sok a zöld, ideális esetben egy körbekerített kert áll rendelkezésére, amiben bármikor kutathat kedvére, s persze őrizheti is területét.'),
('9','Bőr- és szőrzetproblémáktól eltekintve nem hajlamos betegésgekre,megfelelő táplálkozás és elegendő mozgás esetén 16 éves korig is elélhet, akárcsak az őse, a puli.'),
('9','Szőre fésülhető tincses, göndör csigás kinézetű.Legygakoribb színe a szürke, de fekete, fehér, valamint fakó színekben is pompázhat. Ápolása egyszerű, hetente 1x kell megfésülni.'),
('9','Olyan kutyatartónak való, aki szereti a természetet és aktív életmódot él, emellett kellően el tudja foglalni játékos, örökmozgó kutyáját feladatokkal/sportokkal.'),

('10','Rendkívül kitartó és céltudatos jellemű. Erő, elegancia sugárzik belőle, lélekjelenléte óriási. Habár imád futni, nem hiperaktív, de edzett, fáradhatatlan és gyors.'),
('10','Az jelenlegi agár az ősi magyar agárnak és a török/angol agárnak kereszeteződéséből jött létre. Az agarak már 15.000 éve jelen vannak, lovas őseink honfoglalás idejében hozhatták be őket.'),
('10','Régen szarvas, őz, sőt farkas vadászatakor használták a lovas népek, de őrzésre és nyúlfogásra is képesek. Napjainkban már csak a sportversenyeztetés, és kutyakiállítások maradtak számára.'),
('10','A kan súlya kb. 30 kg szokott lenni, a szukáé 25 kg. Vékony, de izmos és rugalmas testfelépítésük miatt rendkívül agilisek, gyorsak, és mozgékony kutyák. Akár 60 km/h-val is futhatnak.'),
('10','Rendkívül értelmes és intelligens, ragaszkodó fajta, de sohasem tolakodó. Gyermekek mellé is javasolt, kiegyensúlyozott természetű, ezért ideális családi kedvenc.'),
('10','Az agár lakásban is tartható, ha megfelelően lemozgatják minden nap. Kifejezetten sokat pihen, akár napi 18 órát is tölthet alvással. Ebből adódóan szereti a kényelmet, a puha fekhelyeket.'),
('10','Nem igényel különleges ápolást, egészséges fajta, nincsenek olyan betegségei, amik kifejezetten jellemzőek lennének rá. Szívós kutyák, az edzetlenség és a túlhevülés okozhat gondot számára.'),
('10','Szőrzete rövid, sűrű, durva, testhez simuló. Lehet fekete, homok vagy hamuszínű, fehér, kissé foltos, enyhén csíkos, cirmos és sárgás is a színe.'),
('10','Nem igényel kifejezetten aktív gazdát, egy átlagos életmódot folytató család is tökéletes számára. Remek kiránduló partner, kedveli a gyerekeket, családbarát kutya.'),

('11','Élénk, barátságos, kiegyensúlyozott, engedelmes, ragaszkodó, könnyen tanítható, magas intelligenciájú kutya, amelynek 2 fajtája van: rövidszőrű és drótszőrű.'),
('11','Ősei valószínűleg a magyar vándorló törzsek vadászkutyái voltak. Az ebeket a nemesek és hadvezérek használták vadászat során. Vizslákra utaló képek már a 10. századból is megtalálhatóak.'),
('11','A drótszőrű vizsla a rövidszőrű és német drótszőrű vizsla keveréke. Azért keresztezték őket, mert a drótszőrűek jobban bírják az időjárás viszontagságait, és ez a bunda segítette őket a vadászatban is.'),
('11','A vizslák problémamentesek, alkalmazkodóak, sokoldalúan használható vadász és munkakutyák. Mezőben, erdőben és vízben is megállják a helyüket, kiváló szaglásuk és vakmerőségük segítik munkájukban.'),
('11','Erőteljesen kötődnek gazdájukhoz, s szívesen alárendelik magukat neki, így kiképzése, nevelése könnyű. Viszont igénylik a törődést, a szeretetet, mert érzékenyek és nem szeretnek egyedül lenni.'),
('11','Szoros barátságot tudnak kiépíteni a gyerekekkel, családbarát kutyák. Szeretik, ha mindenhova mehetnek a családdal, nagy mozgásigényük miatt szaladgálni, sétálni imádnak.'),
('11','Nem alkalmas ketrecben való tartásra, de nem szükséges nagy kerttel bíró ház sem számára. A szűkös családi háznál fontos számára a mindennapi házon kívüli mozgás és leterhelés.'),
('11','Alapvetően mindkét vizslafajta rövid, sűrű, kemény, arany/vörösesbarna színű bundával rendelkező kutyus, akik nem igényelnek különösebb ápolást, csak szőrzetkefélést.'),
('11','Olyan, már nem kezdő kutyatartóknak való, akik kellőképen le tudjá terhelni, és sok időt tudnak fordítani rájuk, mert igényli a kötődést, és mozgást is.');

CREATE TABLE `dogapp`.`questions` 
(`id` INT NOT NULL AUTO_INCREMENT , 
`dog_id` INT NOT NULL , 
`question` VARCHAR(100) NOT NULL , 
`correct` VARCHAR(50) NOT NULL , 
`answer1` VARCHAR(50) NOT NULL , 
`answer2` VARCHAR(50) NOT NULL , 
`answer3` VARCHAR(50) NOT NULL , 
PRIMARY KEY (`id`),
FOREIGN KEY (`dog_id`) REFERENCES dogs(`dog_id`)
) ENGINE = InnoDB;

INSERT INTO `dogapp`.`questions` (`dog_id`,`question`,`correct`,`answer1`,`answer2`,`answer3`)
VALUES
('1','Melyik tulajdonság NEM jellemző a rottweilerre?','bátortalan','idegerős','munkaszerető','hűséges'),
('1','Kik használták tereleőkutyaként a rotikat?','rómaiak','görögök','egyiptomiak','indiaiak'),
('1','Milyen tulajdonságú NE legyen a gazdi, ha rottweilert szeretne?','elfoglalt','tapasztalt','sportos','magabiztos'),

('2','Melyik munkában alkalmazták régen a németjuhászokat leggyakrabban?','pásztorkutya','mentőkutya','rendőrkutya','vakvezető kutya'),
('2','Mi a legfontosabb étel a németjuhász számára?','hús','zöldség-gyümölcs','száraz táp','csontok'),
('2','Németjuhász ápolásakor mit NEM szükséges figyelemmel tartani?','orrát','füleit','szemeit','karmait'),

('3','Mi szükséges ahhoz, hogy a francia bulldogot könnyen taníthassuk?','pozitív megerősítés','hosszú tanórák','megróvás hiba esetén','semmi'),
('3','Milyen betegség NEM jellemző a francia bulldogra?','gerincbetegség','szembetegség','elhízás','horkolás'),
('3','Melyik jellemző NEM illik a francia bulldogokra?','szeret egyedül lenni','jókedvű','tömzsi','okos'),

('4','Mi NEM jellemző a komondor szőrzetére?','puha','dús','hosszú','bozontos'),
('4','Mi az a tulajdonság, ami NEM illik a komondorra?','bizalmas idegenek felé','öntudatos','robosztus','nemes'),
('4','Milyen kutyának NEM mondható a komondor?','városi','természetszerető','vidéki','kis mozgásigényű'),

('5','Mi NEM jellemző a kuvaszra?','közepes méretű','erős','fehér szőrű','hűséges'),
('5','Mit jelenthet a kuvasz neve?','védelmező','nemes','vakmerő','töröktől származó'),
('5','Mi NEM jellemző a kuvasz bundájára?','rövid, nem hullik','sűrű','könnyű ápolni','fésülni kell'),

('6','Melyik kutyától NEM örökölt tulajdonságokat a mudi?','kuvasz','puli','pumi','német spicckutya'),
('6','Melyik tulajdonság az, amely NEM illik a mudira?','nem igényli a mozgást','magabiztos','csibész','hűséges'),
('6','Milyen tulajdonság kis mértékű a mudiban?','vadászösztön','védelmezés','aktivitás','megbízhatóság'),

('7','Milyen hangot ad ki a kopó egy nagy vad elkapása esetén?','mély ugatás','csillingelő hang','nyüszítés','rövid, de hangos vakkantás'),
('7','Milyen céllal volt a leggyakoribb a kopók tartása?','vadászás','sportolás','nyáj terelés','vakvezetés'),
('7','Mi NEM jellemző a kopók szőrére?','puha','fényes','rövid','egyenes szálú'),

('8','Melyik kutyához hasonlít a puli a legjobban?','komondor','kuvasz','mudi','vizsla'),
('8','Jó egészség esetén hány évig élnek a pulik?','akár 16 év','12-14 év','10-12 év','akár 18 év'),
('8','Mi NEM jellemző a pulira?','agresszív','mozgékony','játékos','hangos'),

('9','Melyik kutya feltehetően az őse a puminak?','puli','mudi','komondor','kuvasz'),
('9','Melyik a leggyakoribb színe a puminak?','szürke','fehér','fekete','fakó'),
('9','Melyik tulajdonság NEM jellemző a pumira?','visszahúzódó','eleven','élénk','okos'),

('10','Napjainkban mi a legfőbb foglalkoztatása az agárnak?','Sportversenyek','vadászat','őrzés','nyúlfogás'),
('10','Milyen gyorsan bír futni egy agár?','akár 60 km/h','45-50 km/h','30-40 km/h','60 km/h felett is'),
('10','Mi NEM jellemző az agárra?','tolakodó','agilis','kitartó','ragaszkodó'),

('11','Mi a legfontosabb a vizslák számára?','mozgás','nagy kert','önállóság','pihenés'),
('11','Melyik tulajdonság NEM jellemző a vizslákra?','öntörvényűek','érzékenyek','alkalmazkodóak','könnyen taníthatóak'),
('11','Mi NEM jellemző a vizslák szőrére?','sok ápolást igénylő','rövid','kemény','aranybarna');