//xsuman02

import React from "react";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function About() {
    return (
        <section className="About">
            <div className="row mt-5 px-5">
                <Row className="ml-5">
                    <Col>
                        <h1>Zachraňujeme psy</h1>
                        <p>IIS-ITUtulek pomáhá opuštěným a týraným pejskům nejen nacházet domov, ale také je na tento "nový" život připravit. Útulek těmto psům poskytne dočasný azyl, zahrne je svou péčí a pomůže jim s resocializací a přípravou na začlenění do nových rodin.</p>
                    </Col>

                    <Col>
                        <center>
                            <img src="https://www.thetimes.co.uk/imageserver/image/%2Fmethode%2Ftimes%2Fprod%2Fweb%2Fbin%2F5c150d72-0fd1-11eb-ac14-2688f9fd3309.jpg?crop=3499%2C2333%2C0%2C0" alt="Obrazek 1" />
                        </center>
                    </Col>
                    <p className="mt-2">Všichni jsme nadšení milovníci psů. Zajímají nás novinky v oblasti chovu, výcviku, výživy i celkové péči o psy. A to hlavně proto, aby se všichni naši dočasní svěřenci v útulku měli příjemně do doby, než najdou své domovy.</p>
                </Row>

                <Row className="ml-5 mt-5">
                    <Col>
                        <h2 className="mt-2">Staňte se dobrovolníkem</h2>
                        <ul className="ml-5">
                            <li>
                                Vyplňte registraci pro žádost o status dobrovolníka.
                            </li>
                            <li>
                                Vyčkejte na potvrzení z naší strany.
                            </li>
                            <li>
                                Užívejte si čas strávený s mazlíčky :)
                            </li>
                        </ul>
                    </Col>
                    <Col>
                        <h2 className="mt-2">Kde nás najdete</h2>
                        <ul className="ml-5">
                            <li>
                                Palcary 1186/50, 635 00 Brno-Komín
                            </li>
                            <li>
                                +420 777 666 555
                            </li>
                            <li>
                                iisitutulek@utulek.cz
                            </li>
                            <li>
                                Fungujeme nonstop! :)
                            </li>

                        </ul>
                    </Col>
                </Row>
            </div>


        </section>
    );
}