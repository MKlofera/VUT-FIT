/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React from "react";
import Form from "react-bootstrap/Form";
import DualRangeSlider from "./Base/DualRangeSliderComponent";
import SexCheckButtonComponent from "./Base/SexCheckButtonComponent";
import { useState, useRef } from "react";
import { Col, Row } from "react-bootstrap";

export default function DogListFilterComponent(props) {
    const DogMinAge = 1;
    const DogMaxAge = 20;
    const DogMinPrice = 500;
    const DogMaxPrice = 3500;

    const [FormInput, SetFormInput] = useState("");
    const [Sex, setSex] = useState("both");
    const [Breed, setBreed] = useState("vybrat");
    const MaxAge = useRef(DogMinAge);
    const MinAge = useRef(DogMaxAge);
    const MinPrice = useRef(DogMinPrice);
    const MaxPrice = useRef(DogMaxPrice);

    const handleSex = (SelectedSex) => {
        setSex(SelectedSex);
    };
    const handleBreed = (e) => {
        setBreed(e.target.value);
    };
    const sendToParent = () => {
        props.getFilters(
            Breed,
            MinAge.current,
            MaxAge.current,
            MinPrice.current,
            MaxPrice.current,
            Sex,
            FormInput
        );
    };

    return (
        <Row>
            <Col xl={2} lg={4} md={4}>
                <div className="m-3">
                    <Form.Label>Plemeno</Form.Label>
                    <Form.Select onChange={handleBreed}>
                        <option>Vybrat</option>
                        {props.Selector.map((element, index) => {
                            return (
                                <option value={element.breed} key={index}>
                                    {element.name}
                                </option>
                            );
                        })}
                    </Form.Select>
                </div>
            </Col>
            <div className="col-xl-2 col-lg-4 col-md-4 col-sm-6 d-flex justify-content-center">
                <div className="flex-column m-3">
                    <Form.Label className="mb-4">Věk</Form.Label>
                    <DualRangeSlider
                        min={DogMinAge}
                        max={DogMaxAge}
                        onChange={({ min, max }) => {
                            MinAge.current = min;
                            MaxAge.current = max;
                        }}
                    />
                </div>
            </div>
            <div className="col-xl-2 col-lg-4 col-md-4 col-sm-6 d-flex justify-content-center">
                <div className="flex-column m-3">
                    <Form.Label className="mb-4">Cena</Form.Label>
                    <DualRangeSlider
                        min={DogMinPrice}
                        max={DogMaxPrice}
                        onChange={({ min, max }) => {
                            MinPrice.current = min;
                            MaxPrice.current = max;
                        }}
                    />
                </div>
            </div>

            <div className="col-xl-2 col-lg-6 col-md-6 col-sm-12">
                <div className="m-3">
                    <Form.Label>Pohlaví</Form.Label>
                    <SexCheckButtonComponent
                        handleSex={handleSex}
                    ></SexCheckButtonComponent>
                </div>
            </div>

            <div className="col-xl-2 col-lg-6 col-md-6">
                <div className="m-3">
                    <Form.Label>Jméno</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Punťa"
                        onChange={(e) => SetFormInput(e.target.value)}
                    />
                </div>
            </div>

            <div className="col-xl-2 col-12">
                <div className="mt-5 mx-3">
                    <Form.Label></Form.Label>
                    {/* OnClick - Just to re-render page to get actual data about age and price */}
                    <button className="button active" onClick={sendToParent}>
                        {" "}
                        Vyhledat
                    </button>
                </div>
            </div>
        </Row>
    );
}
