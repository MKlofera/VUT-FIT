-- SQL skript pro vytvoření základních objektů schématu databáze
-- Zadání č. 33 – Lékarna

-- Autor1: Tomáš Szabó <xszabo16@stud.fit.vutbr.cz> (Vedoucí)
-- Autor2: Marek Klofera <xklofe01@stud.fit.vutbr.cz>

drop table objednavka_has_lek;
drop table objednavka;
drop table dodavatel;
drop table typ_stavu_objednavky;
drop table lek_has_zaznam;
drop table zaznam_o_prodeji;
drop table lek_has_formu_uziti;
drop table lek_has_alergen;
drop table sleva_na_lek;
drop table lek;
drop table typ_formy_uziti;
drop table typ_konzumenta;
drop table typ_uziti_leku;
drop table typ_alergenu;
drop table typ_leku;
drop table pojistovna;
drop table pracovnik;
drop table pracovnik_typ_pozice;

CREATE TABLE pracovnik_typ_pozice
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE pracovnik
(
    r_cislo       CHAR(15) PRIMARY KEY
        CHECK (REGEXP_LIKE(
                r_cislo, '[0-9]{2,10}\/[0-9]{4}'
            )),
    krestni_jmeno VARCHAR(80)                         NOT NULL,
    prijmeni      VARCHAR(80)                         NOT NULL,
    ulice         VARCHAR(255)                        NOT NULL,
    mesto         VARCHAR(255)                        NOT NULL,
    psc           CHAR(5)                             NOT NULL,
    telefon       VARCHAR(40)                         NOT NULL,
    pozice        INT references pracovnik_typ_pozice NOT NULL
);

CREATE TABLE pojistovna
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL,
    ICO   CHAR(8)      NOT NULL
);

CREATE TABLE typ_leku
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);

CREATE TABLE typ_alergenu
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE typ_uziti_leku
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE typ_konzumenta
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE typ_formy_uziti
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE lek
(
    id_lek            INT generated as identity PRIMARY KEY,
    id_typ_leku       INT references typ_leku       NOT NULL,
    nazev             VARCHAR(255)                  NOT NULL,
    cena              DECIMAL(10, 2)                NOT NULL,
    trvalivost        DATE                          NOT NULL,
    id_uziti_leku     INT references typ_uziti_leku NOT NULL,
    id_konzumenta     INT references typ_konzumenta NOT NULL,
    aktualni_pocet_ks INT                           NOT NULL

);
CREATE TABLE sleva_na_lek
(
    id            INT generated as identity,
    id_lek        INT references lek        NOT NULL,
    id_pojistovna INT references pojistovna NOT NULL,
    datum         DATE,
    vyse_slevy    INT,
    PRIMARY KEY (id, id_lek, id_pojistovna)
);
CREATE TABLE lek_has_alergen
(
    id_lek     INT references lek          NOT NULL,
    id_alergen INT references typ_alergenu NOT NULL,
    PRIMARY KEY (id_lek, id_alergen)
);
CREATE TABLE lek_has_formu_uziti
(
    id_lek         INT references lek             NOT NULL,
    id_formy_uziti INT references typ_formy_uziti NOT NULL,
    PRIMARY KEY (id_lek, id_formy_uziti)
);

CREATE TABLE zaznam_o_prodeji
(
    id           INT generated as identity PRIMARY KEY,
    datum        DATE,
    id_pracovnik CHAR(15) references pracovnik NOT NULL

);
CREATE TABLE lek_has_zaznam
(
    id_lek               INT references lek              NOT NULL,
    id_zaznamu_o_prodeji INT references zaznam_o_prodeji NOT NULL,
    pocet_ks             INT                             NOT NULL,
    PRIMARY KEY (id_lek, id_zaznamu_o_prodeji)
);
CREATE TABLE typ_stavu_objednavky
(
    id    INT generated as identity PRIMARY KEY,
    nazev VARCHAR(255) NOT NULL
);
CREATE TABLE dodavatel
(
    id      INT generated as identity PRIMARY KEY,
    ICO     CHAR(8)      NOT NULL,
    nazev   VARCHAR(255) NOT NULL,
    email   VARCHAR(255) NOT NULL
        CHECK (REGEXP_LIKE(
                email, '^[a-z]+[a-z0-9\.]*@[a-z0-9\.-]+\.[a-z]{2,}$', 'i'
            )),
    telefon VARCHAR(40)  NOT NULL
);
CREATE TABLE objednavka
(
    id                    INT generated as identity PRIMARY KEY,
    pocet_ks              INT                                 NOT NULL,
    cena_za_ks            DECIMAL(10, 2)                      NOT NULL,
    datum_prevzeti        DATE,
    datum_objednani       DATE                                NOT NULL,
    stav                  INT references typ_stavu_objednavky NOT NULL,
    id_pracovnik_objednal CHAR(15) references pracovnik       NOT NULL,
    id_pracovnik_prevzal  CHAR(15) references pracovnik
);
CREATE TABLE objednavka_has_lek
(
    id_objednavka INT references objednavka NOT NULL,
    id_lek        INT references lek        NOT NULL,
    PRIMARY KEY (id_objednavka, id_lek)
);

INSERT INTO pracovnik_typ_pozice (nazev)
VALUES ('Vedouci');
INSERT INTO pracovnik_typ_pozice (nazev)
VALUES ('Farmaceut');

INSERT INTO pracovnik (r_cislo, krestni_jmeno, prijmeni, ulice, mesto, psc, telefon, pozice)
VALUES ('1234567890/0600', 'Jarda', 'Kozlik', 'Národní', 'Brno', '63400', '775678987', 1);
INSERT INTO pracovnik (r_cislo, krestni_jmeno, prijmeni, ulice, mesto, psc, telefon, pozice)
VALUES ('1234567801/0600', 'Rumcajs', 'Novák', 'Dubová', 'Řáholec', '23400', '123235345', 2);

INSERT INTO pojistovna (nazev, ICO)
VALUES ('Vojenská', '12345678');
INSERT INTO pojistovna (nazev, ICO)
VALUES ('MINISTRA VNITRA', '87654321');


INSERT INTO typ_leku (nazev)
VALUES ('Volný prodej');
INSERT INTO typ_leku (nazev)
VALUES ('Na předpis');

INSERT INTO typ_alergenu (nazev)
VALUES ('Korýši');
INSERT INTO typ_alergenu (nazev)
VALUES ('Laktóza');

INSERT INTO typ_uziti_leku (nazev)
VALUES ('Proti bolesti');
INSERT INTO typ_uziti_leku (nazev)
VALUES ('Nadymavost');

INSERT INTO typ_konzumenta (nazev)
VALUES ('Děti');
INSERT INTO typ_konzumenta (nazev)
VALUES ('Dospelí');

INSERT INTO typ_formy_uziti (nazev)
VALUES ('Orální');
INSERT INTO typ_formy_uziti (nazev)
VALUES ('Kožní');

INSERT INTO lek (id_typ_leku, nazev, cena, trvalivost, id_uziti_leku, id_konzumenta, aktualni_pocet_ks)
VALUES (2, 'Paralen', 99.90, TO_DATE('2025-07-30', 'yyyy/mm/dd'), 1, 1, 5000);
INSERT INTO lek (id_typ_leku, nazev, cena, trvalivost, id_uziti_leku, id_konzumenta, aktualni_pocet_ks)
VALUES (1, 'Xyzal', 150.90, TO_DATE('2025-04-30', 'yyyy/mm/dd'), 1, 2, 3000);

INSERT INTO sleva_na_lek (id_lek, id_pojistovna, datum, vyse_slevy)
VALUES (1, 1, TO_DATE('2020-08-30', 'yyyy/mm/dd'), 50);
INSERT INTO sleva_na_lek (id_lek, id_pojistovna, datum, vyse_slevy)
VALUES (1, 2, TO_DATE('2021-04-23', 'yyyy/mm/dd'), 40);

INSERT INTO lek_has_alergen (id_lek, id_alergen)
VALUES (1, 2);
INSERT INTO lek_has_alergen (id_lek, id_alergen)
VALUES (2, 1);

INSERT INTO lek_has_formu_uziti (id_lek, id_formy_uziti)
VALUES (1, 1);
INSERT INTO lek_has_formu_uziti (id_lek, id_formy_uziti)
VALUES (2, 1);

INSERT INTO zaznam_o_prodeji (datum, id_pracovnik)
VALUES (TO_DATE('2022-02-01', 'yyyy/mm/dd'), '1234567890/0600');
INSERT INTO zaznam_o_prodeji (datum, id_pracovnik)
VALUES (TO_DATE('2021-04-02', 'yyyy/mm/dd'), '1234567801/0600');

INSERT INTO lek_has_zaznam (id_lek, id_zaznamu_o_prodeji, pocet_ks)
VALUES (1, 1, 20);
INSERT INTO lek_has_zaznam (id_lek, id_zaznamu_o_prodeji, pocet_ks)
VALUES (2, 2, 40);

INSERT INTO typ_stavu_objednavky (nazev)
VALUES ('Doručeno');
INSERT INTO typ_stavu_objednavky (nazev)
VALUES ('Objednáno');

INSERT INTO dodavatel (ICO, nazev, email, telefon)
VALUES ('12345678', 'Levné léky s.r.o.', 'levne@leky.cz', '123456789');
INSERT INTO dodavatel (ICO, nazev, email, telefon)
VALUES ('87654321', 'Drahé léky s.r.o.', 'drahe@leky.cz', '776898765');

INSERT INTO objednavka (pocet_ks, cena_za_ks, datum_prevzeti, datum_objednani, stav, id_pracovnik_objednal,
                        id_pracovnik_prevzal)
VALUES (200, 0.90, TO_DATE('2021-04-02', 'yyyy/mm/dd'), TO_DATE('2020-04-02', 'yyyy/mm/dd'), 1, '1234567801/0600',
        '1234567801/0600');
INSERT INTO objednavka (pocet_ks, cena_za_ks, datum_prevzeti, datum_objednani, stav, id_pracovnik_objednal,
                        id_pracovnik_prevzal)
VALUES (3000, 1.90, NULL, TO_DATE('2021-04-02', 'yyyy/mm/dd'), 2, '1234567801/0600', NULL);

INSERT INTO objednavka_has_lek (id_objednavka, id_lek)
VALUES (1, 1);
INSERT INTO objednavka_has_lek (id_objednavka, id_lek)
VALUES (2, 1);

-- zobrazí všechny léky, které mají nějakou slevu od pojišťovny
-- select využívající spojení dvou tabulek
SELECT id_lek, nazev, vyse_slevy, cena
FROM lek NATURAL JOIN sleva_na_lek;

-- zobrazí všechny pracovníky a jejich pracovní pozice
-- select využívající spojení dvou tabulek
SELECT prac.krestni_jmeno, prac.prijmeni, pozic.nazev
FROM pracovnik prac JOIN pracovnik_typ_pozice pozic
ON prac.pozice = pozic.id;

-- Nejdražší objednavka leku
-- select využívající spojení tří tabulek
SELECT nazev , max(cena_za_ks * pocet_ks)
FROM lek NATURAL JOIN objednavka_has_lek NATURAL JOIN objednavka
GROUP BY nazev;

-- zobrazí kolik mají jednotlivý zaměstnanci prodejů
-- select s klauzulí GROUP BY a agregační funkcí
SELECT p.krestni_jmeno , p.prijmeni, count(*)
FROM Pracovnik p JOIN zaznam_o_prodeji zop
ON p.r_cislo = zop.id_pracovnik
GROUP BY p.r_cislo, p.krestni_jmeno, p.prijmeni;

-- Suma ceny kolik jednotlivý zaměstnanci prodali léků
-- select s klauzulí GROUP BY a agregační funkcí
SELECT p.krestni_jmeno , p.prijmeni, SUM(lhz.pocet_ks * lek.cena)
FROM Pracovnik p JOIN zaznam_o_prodeji zop
ON p.r_cislo = zop.id_pracovnik
JOIN lek_has_zaznam lhz
ON lhz.id_zaznamu_o_prodeji = zop.id
JOIN lek
ON lhz.id_lek = lek.id_lek
GROUP BY p.r_cislo, p.krestni_jmeno, p.prijmeni;

-- Zobrazí léky, které byly někdy objednané
-- select obsahující predikát EXISTS
SELECT nazev, cena FROM lek
WHERE EXISTS
    (SELECT id_lek FROM objednavka_has_lek
    WHERE lek.id_lek = objednavka_has_lek.id_lek);

-- Zobrazí léky, které jsou určeny k volnému prodeji
-- select s predikátem IN s vnořeným selectem
SELECT nazev, cena FROM lek
WHERE id_typ_leku IN
      (SELECT id FROM typ_leku
        WHERE typ_leku.nazev = 'Volný prodej');
