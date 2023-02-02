//xsuman02

import React from "react";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function Support() {
    return (
        <section className="Support">
            <div className="row mt-5 px-5">
                <Row className="ml-5">
                    <Col>
                        <h1>Podpořte náš útulek</h1>
                        <p>Veterinární péče o naše psy je finančně náročná. Váš příspěvek pomůže uhradit výdaje související s ošetřením zranění, očkováním nebo odčervováním nalezených psů.</p>
                        <p>Provoz útulku je finančně náročný. Proto uvítáme jakoukoli materiální pomoc, která nám ušetří peníze. Ty můžeme následně investovat do péče o naše pejsky.</p>
                    </Col>
                    <Col className="mt-5">
                        <center>
                            <img src="http://www.petplan.com.au/blog/wp-content/uploads/2016/12/pet-insurance-dog-cat-8.jpg" alt="Obrazek 1" />
                        </center>
                    </Col>
                </Row>

                <Row className="ml-5 mt-5">
                    <Col>
                        <h2>Staň se psím partnerem</h2>
                        <p>Sponzorování či adopce jsou v těchto dnech tou nejlepší cestou, jak můžete podpořit svého oblíbeného pejska i útulek jako celek. Za veškeré projevy přízně vám velmi děkujeme!</p>
                    </Col>
                    <Col className="mt-5">
                        <center>
                            <img src="https://i.ibb.co/SR1jKJx/transparent-account.png" alt="Obrazek 2" />
                        </center>
                    </Col>
                </Row>
                <Row className="ml-5 mt-5">
                    <Col>
                        <h2>Máš nějaký nápad, jak pejskům pomoci?</h2>
                        <p>Máš jakýkoli nápad, myšlenku, či názor, se kterým by jsi se s námi rád podělil? Využij tento formulář! :)</p>
                    </Col>
                    <Col className="mt-5 mr-5 ml-5">
                        <center>
                            <form>
                                <Row>
                                    <Col>
                                        <input type="text" className="form-control" placeholder="Jméno"></input>
                                    </Col>
                                    <Col className="ml-5 width-100">
                                        <input type="text" className="form-control" placeholder="Email"></input>
                                    </Col>

                                    <textarea id="feedback_form" rows="4" cols="50" className="mt-3">
                                        Jak se můžeme zlepšit?
                                    </textarea>
                                    <button type="submit" className="btn btn-primary mt-2">Odeslat</button>

                                </Row>
                            </form>
                        </center>
                    </Col>
                </Row>
            </div>


        </section>
    );
}