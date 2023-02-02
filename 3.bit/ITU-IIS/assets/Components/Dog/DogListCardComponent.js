/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React from "react";

//bootstrap
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { Link } from "react-router-dom";

const DogListCardComponent = (props) => {
    return (
        <Card className="DogListCardComponent m-3" style={{ width: "18rem" }}>
            <div className="image">
                <Link to={`dog/${props.id}`}>
                    <Card.Img variant="top" src={props.photo} />
                </Link>
            </div>
            <Link to={`dog/${props.id}`}>
                <Card.Body>
                    <Card.Title>{props.name}</Card.Title>
                    <Card.Text>
                        <div className="d-flex flex-column">
                            <span>Věk: {props.age} let</span>
                            <span>Plemeno: {EnumBreed[props.breed]}</span>
                            <div className="d-flex justify-content-between">
                                <span>
                                    Pohlaví: {props.sex ? "Pes" : "Fena"}
                                </span>
                                <span className="price">{props.price},-Kč</span>
                            </div>
                        </div>
                    </Card.Text>
                </Card.Body>
            </Link>
        </Card>
    );
};

export default DogListCardComponent;
const EnumBreed = [
    "Afgánský chrt",
    "Anglický buldok",
    "Argentinská doga",
    "Belgický ovčák",
    "Bígl",
    "Border kolie",
    "Bulteriér",
    "Československý vlčák",
    "Dalmatin",
    "Dobrman",
    "Holandský ovčák",
    "Jezevčík",
    "Německý ovčák",
    "Shiba-Inu",
    "Zlatý retriever",
];
