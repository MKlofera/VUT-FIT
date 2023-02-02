/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React from "react";
import { useState } from "react";
import Form from "react-bootstrap/Form";

export default function VetFilterComponent(props) {
    const [Date, setDate] = useState(null);
    const [VetId, setVetId] = useState(null);
    const [Type, setType] = useState(null);

    const sendToParent = () => {
        props.getFilters(Date, VetId, Type);
    };

    // TODO - delete this props var and use props directly
    props = {
        vets: [
            {
                id: 1,
                name: "Ferda Ferkovič",
            },
            {
                id: 2,
                name: "Pepa Novák",
            },
            {
                id: 3,
                name: "Jana Nová",
            },
        ],
        VetTypes: [
            {
                id: 1,
                type: "Vaccinnation",
            },
            {
                id: 2,
                type: "Surgery",
            },
            {
                id: 3,
                type: "Castration",
            },
        ],
    };

    const handleVet = (e) => {
        setVetId(e.target.value);
    }
    const handleType = (e) => {
        setType(e.target.value);
    };
    const handleDate = (e) => {
        setDate(e.target.value);
    };

    return (
        <div className="row">
            <div className="p-2 col-xl-3 col-lg-3 col-md-3 col-sm-4">
                <Form.Label>Veterinář</Form.Label>
                <Form.Select onChange={handleVet}>
                    <option>vybrat</option>
                    {props.vets.map((element, index) => {
                        return (
                            <option value={element.breed}>
                                {element.name}
                            </option>
                        );
                    })}
                </Form.Select>
            </div>
            <div className="p-2 col-xl-3 col-lg-3 col-md-3 col-sm-4">
                <Form.Label>Typ zákroku</Form.Label>
                <Form.Select onChange={handleType}>
                    <option>Vybrat</option>
                    {props.VetTypes.map((element, index) => {
                        return (
                            <option value={element.breed}>
                                {element.name}
                            </option>
                        );
                    })}
                </Form.Select>
            </div>
            <div className="p-2 col-xl-3 col-lg-3 col-md-3 col-sm-4">
                <Form.Label>Datum zákroku</Form.Label>
                <Form.Control type="date" onChange={(e) => handleDate(e)} />
            </div>

            <div className="p-2 col-xl-3 col-lg-3 col-md-3 col-sm-12 d-flex flex-column justify-content-end">
                <button className="button active">Vyhledat</button>
            </div>
        </div>
    );
}
