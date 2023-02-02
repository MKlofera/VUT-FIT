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
    pocet_ks      INT                       NOT NULL,
    cena_za_ks    DECIMAL(10, 2)            NOT NULL,

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

INSERT INTO objednavka (datum_prevzeti, datum_objednani, stav, id_pracovnik_objednal,
                        id_pracovnik_prevzal)
VALUES (TO_DATE('2021-04-02', 'yyyy/mm/dd'), TO_DATE('2020-04-02', 'yyyy/mm/dd'), 1, '1234567801/0600',
        '1234567801/0600');
INSERT INTO objednavka (datum_prevzeti, datum_objednani, stav, id_pracovnik_objednal,
                        id_pracovnik_prevzal)
VALUES (NULL, TO_DATE('2021-04-02', 'yyyy/mm/dd'), 2, '1234567801/0600', NULL);

INSERT INTO objednavka_has_lek (id_objednavka, id_lek, pocet_ks, cena_za_ks)
VALUES (1, 1, 3000, 1.90);
INSERT INTO objednavka_has_lek (id_objednavka, id_lek, pocet_ks, cena_za_ks)
VALUES (2, 1, 200, 0.90);

-- TRIGGERS --
------------------------------------------------------------------------------------------------------------------------
CREATE
OR REPLACE TRIGGER add_current_count_of_lek
AFTER INSERT ON objednavka_has_lek
FOR EACH ROW
DECLARE AKTUALNI_POCET_KS INT;
BEGIN
SELECT aktualni_pocet_ks INTO AKTUALNI_POCET_KS FROM lek
WHERE lek.id_lek = :NEW.id_lek;


        UPDATE lek set lek.aktualni_pocet_ks = AKTUALNI_POCET_KS + :NEW.pocet_ks
        WHERE lek.id_lek = :NEW.id_lek;
END;

-- testovani triggeru
-- INSERT INTO objednavka (datum_prevzeti, datum_objednani, stav, id_pracovnik_objednal,
--                         id_pracovnik_prevzal)
-- VALUES (TO_DATE('2021-04-02', 'yyyy/mm/dd'), TO_DATE('2020-04-02', 'yyyy/mm/dd'), 1, '1234567801/0600',
--         '1234567801/0600');
--
-- INSERT INTO objednavka_has_lek (id_objednavka, id_lek, pocet_ks, cena_za_ks)
-- VALUES (3, 1, 10000, 99.90);
--
-- SELECT aktualni_pocet_ks FROM lek WHERE id_lek = 1;

CREATE OR REPLACE TRIGGER odd_current_count_of_lek
AFTER INSERT ON lek_has_zaznam
FOR EACH ROW
DECLARE AKTUALNI_POCET_KS INT;
BEGIN

SELECT aktualni_pocet_ks INTO AKTUALNI_POCET_KS FROM lek
WHERE lek.id_lek = :NEW.id_lek;

    IF AKTUALNI_POCET_KS < :NEW.pocet_ks THEN
            RAISE_APPLICATION_ERROR(-20420, 'Pokus o prodej léků, který není na skladě.');
    ELSE
        UPDATE lek set lek.aktualni_pocet_ks = aktualni_pocet_ks - :NEW.pocet_ks
        WHERE lek.id_lek = :NEW.id_lek;
    END IF;
END;
-- testovani triggeru
-- INSERT INTO zaznam_o_prodeji (datum, id_pracovnik)
-- VALUES (TO_DATE('2022-02-01', 'yyyy/mm/dd'), '1234567890/0600');
--
-- INSERT INTO lek_has_zaznam (id_lek, id_zaznamu_o_prodeji, pocet_ks)
-- VALUES (1, 4, 16000);
--
-- SELECT aktualni_pocet_ks FROM lek WHERE id_lek = 1;

-- PROCEDURES --
------------------------------------------------------------------------------------------------------------------------
CREATE OR REPLACE PROCEDURE "prehled_prodeju_pracovniku"
AS
    "pocet_prodanych_leku" NUMBER;
    "pocet_zaznamu_o_prodeji" NUMBER;
    "prumerny_pocet_leku_v_zaznamu" NUMBER;
BEGIN
    SELECT SUM(pocet_ks) INTO "pocet_prodanych_leku" FROM lek_has_zaznam;
    SELECT COUNT(*) INTO "pocet_zaznamu_o_prodeji" FROM zaznam_o_prodeji;

    "prumerny_pocet_leku_v_zaznamu" := "pocet_prodanych_leku" / "pocet_zaznamu_o_prodeji";

    DBMS_OUTPUT.put_line(
		'prehled: '
		|| "pocet_prodanych_leku"           || ' leků se prodalo, '
		|| "pocet_zaznamu_o_prodeji"        || ' prodeju bylo zaznamenano, '
		|| "prumerny_pocet_leku_v_zaznamu"  || ' byl prumerny pocet leku na zaznam.'
	);

    EXCEPTION WHEN ZERO_DIVIDE THEN
	BEGIN
		IF "pocet_zaznamu_o_prodeji" = 0 THEN
			DBMS_OUTPUT.put_line('Žádné záznamy nebyly předány');
		END IF;
	END;
end;

begin "prehled_prodeju_pracovniku";
end;

CREATE OR REPLACE PROCEDURE "has_alergen_of_type"
	("nazev_alergenu" IN VARCHAR)
AS
    "vsechny_leky" NUMBER;
	"lek_s_typem" NUMBER;
	"typ_alergenu_id" typ_alergenu.id%TYPE;
	"id_typu_alergenu" typ_alergenu.id%TYPE;
	CURSOR "cursor_alergeny" IS SELECT id_alergen FROM lek_has_alergen;
BEGIN
	SELECT COUNT(*) INTO "vsechny_leky" FROM lek;

	"lek_s_typem" := 0;

	SELECT id INTO "id_typu_alergenu"
	FROM typ_alergenu
	WHERE nazev = "nazev_alergenu";

	OPEN "cursor_alergeny";
	LOOP
		FETCH "cursor_alergeny" INTO "typ_alergenu_id";

		EXIT WHEN "cursor_alergeny"%NOTFOUND;

		IF "typ_alergenu_id" = "id_typu_alergenu" THEN
			"lek_s_typem" := "lek_s_typem" + 1;
		END IF;
	END LOOP;
	CLOSE "cursor_alergeny";

	DBMS_OUTPUT.put_line(
		'typ alergernu:  ' || "nazev_alergenu" || ' obsahuje ' || "lek_s_typem"
		|| ' leků z ' || "vsechny_leky" || ' léků.'
	);

	EXCEPTION WHEN NO_DATA_FOUND THEN
	BEGIN
		DBMS_OUTPUT.put_line(
			'typ ' || "nazev_alergenu" || ' nemá zástupce v lécích.'
		);
	END;
END;
-- Testování procedůry
-- begin "has_alergen_of_type"('super');
-- end;
-- begin "has_alergen_of_type"('Korýši');
-- end;
-- begin "has_alergen_of_type"('Laktóza');
-- end;

-- INDEX --
------------------------------------------------------------------------------------------------------------------------
-- Suma ceny kolik jednotlivý zaměstnanci prodali léků
-- select s klauzulí GROUP BY a agregační funkcí
EXPLAIN PLAN FOR
SELECT p.krestni_jmeno , p.prijmeni, SUM(lhz.pocet_ks * lek.cena)
FROM Pracovnik p JOIN zaznam_o_prodeji zop
ON p.r_cislo = zop.id_pracovnik
JOIN lek_has_zaznam lhz
ON lhz.id_zaznamu_o_prodeji = zop.id
JOIN lek
ON lhz.id_lek = lek.id_lek
GROUP BY p.r_cislo, p.krestni_jmeno, p.prijmeni;
SELECT * FROM table ( DBMS_XPLAN.DISPLAY );

CREATE INDEX "p.krestni_jmeno" ON Pracovnik (krestni_jmeno); -- index

EXPLAIN PLAN FOR
SELECT p.krestni_jmeno , p.prijmeni, SUM(lhz.pocet_ks * lek.cena)
FROM Pracovnik p JOIN zaznam_o_prodeji zop
ON p.r_cislo = zop.id_pracovnik
JOIN lek_has_zaznam lhz
ON lhz.id_zaznamu_o_prodeji = zop.id
JOIN lek
ON lhz.id_lek = lek.id_lek
GROUP BY p.r_cislo, p.krestni_jmeno, p.prijmeni;
SELECT * FROM table ( DBMS_XPLAN.DISPLAY );

-- POHLED --
------------------------------------------------------------------------------------------------------------------------
CREATE MATERIALIZED VIEW "prehled_prodeje" AS
SELECT zop.id, lek.nazev
FROM zaznam_o_prodeji zop, lek, lek_has_zaznam lhz
WHERE zop.id = lhz.id_zaznamu_o_prodeji AND lek.id_lek = lhz.id_lek;

SELECT * FROM "prehled_prodeje"; -- před
UPDATE lek SET nazev = 'Diozepan' WHERE id_lek = 1;
SELECT * FROM "prehled_prodeje"; -- po

-- PRÁVA --
------------------------------------------------------------------------------------------------------------------------
GRANT ALL ON objednavka_has_lek TO xklofe01;
GRANT ALL ON objednavka TO xklofe01;
GRANT ALL ON dodavatel TO xklofe01;
GRANT ALL ON typ_stavu_objednavky TO xklofe01;
GRANT ALL ON lek_has_zaznam TO xklofe01;
GRANT ALL ON zaznam_o_prodeji TO xklofe01;
GRANT ALL ON lek_has_formu_uziti TO xklofe01;
GRANT ALL ON lek_has_alergen TO xklofe01;
GRANT ALL ON sleva_na_lek TO xklofe01;
GRANT ALL ON lek TO xklofe01;
GRANT ALL ON typ_formy_uziti TO xklofe01;
GRANT ALL ON typ_konzumenta TO xklofe01;
GRANT ALL ON typ_uziti_leku TO xklofe01;
GRANT ALL ON typ_alergenu TO xklofe01;
GRANT ALL ON typ_leku TO xklofe01;
GRANT ALL ON pojistovna TO xklofe01;
GRANT ALL ON pracovnik TO xklofe01;
GRANT ALL ON pracovnik_typ_pozice TO xklofe01;
GRANT ALL ON "prehled_prodeje" TO xklofe01;
GRANT EXECUTE ON "prehled_prodeju_pracovniku" TO xklofe01;
GRANT EXECUTE ON "has_alergen_of_type" TO xklofe01;