/**
 * author: Marek Klofera
 * login: xklofe01
 */
import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";

// outsources
import ImageGallery from "react-image-gallery";

export default function DogDetailEditInfoComponent(props) {
    const [DogDetail, setDogDetail] = useState(props.DogDetail);

    function onInputchange(event) {
        setDogDetail({
            ...DogDetail,
            [event.target.name]: event.target.value,
        });
    }

    useEffect(() => {
        props.SendToParentEditedDog(DogDetail);
    }, [DogDetail]);

    return (
        <section className="DogDetailInfoComponent">
            <div className="row mt-3">
                <div className="col-xl-4 col-lg-6 px-3">
                    {/* <img
                        src={DogDetail.Photo}
                        alt="dog"
                        className="img-fluid"
                    />
                    <div className="m-3">
                        <input
                            className="form-control"
                            type="file"
                            id="formFile"
                        />
                    </div> */}
                </div>
                {/* <ImageGallery items={images} /> TODO consider more than one image*/}
                <div className="col-xl-8 col-lg-6 px-3">
                    <div className="form-group p-1">
                        <label>Jméno psa</label>
                        <input
                            type="text"
                            name="Name"
                            className="form-control"
                            value={DogDetail.Name}
                            onChange={onInputchange}
                        />
                    </div>
                    <div className="form-group p-1">
                        <label>Důvod umístění do útulku</label>
                        <select
                            className="form-select"
                            name="InShelterBcs"
                            onChange={onInputchange}
                        >
                            {EnumInShelterBcs.map((item, index) => {
                                return (
                                    <React.Fragment key={index}>
                                        <option
                                            value={index}
                                            selected={
                                                index === DogDetail.InShelterBcs
                                            }
                                        >
                                            {item}
                                        </option>
                                    </React.Fragment>
                                );
                            })}
                            ;
                        </select>
                    </div>
                    <div className="row">
                        <div className="col-6">
                            <div className="form-group p-1">
                                <label>Věk</label>
                                <input
                                    type="number"
                                    value={DogDetail.Age}
                                    name="Age"
                                    className="form-control"
                                    onChange={onInputchange}
                                />
                            </div>
                            <div className="form-group p-1">
                                <label>Pohlaví</label>
                                <select
                                    className="form-select"
                                    name="Sex"
                                    onChange={onInputchange}
                                >
                                    {EnumSex.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        DogDetail.InShelterBcs
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                    ;
                                </select>
                            </div>
                            <div className="form-group p-1">
                                <label>Plemeno</label>
                                <select
                                    className="form-select"
                                    name="Breed"
                                    onChange={onInputchange}
                                >
                                    {EnumBreed.map((item, index) => {
                                        return (
                                            <React.Fragment key={index}>
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        DogDetail.InShelterBcs
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            </React.Fragment>
                                        );
                                    })}
                                    ;
                                </select>
                            </div>
                            <div className="form-group p-1">
                                <label>Číslo čipu</label>
                                <input
                                    type="number"
                                    className="form-control"
                                    name="ChipNumber"
                                    value={DogDetail.ChipNumber}
                                    defaultValue
                                    onChange={onInputchange}
                                />
                            </div>
                        </div>
                        <div className="col-6 rightColumnInfo d-flex flex-column justify-content-between">
                            <div>
                                <div className="form-group p-1">
                                    <label>Odčervení</label>
                                    <select
                                        className="form-select"
                                        name="Dewormed"
                                        onChange={onInputchange}
                                    >
                                        {YesNo.map((item, index) => {
                                            return (
                                                <React.Fragment key={index}>
                                                    <option
                                                        value={index}
                                                        selected={
                                                            index ===
                                                            DogDetail.Dewormed
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                </div>
                                <div className="form-group p-1">
                                    <label>Očkování</label>
                                    <select
                                        className="form-select"
                                        onChange={onInputchange}
                                        name="Vaccinated"
                                    >
                                        {YesNo.map((item, index) => {
                                            return (
                                                <React.Fragment key={index}>
                                                    <option
                                                        value={index}
                                                        selected={
                                                            index ===
                                                            DogDetail.Vaccinated
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                </div>
                                <div className="form-group p-1">
                                    <label>Kastrace</label>
                                    <select
                                        className="form-select"
                                        name="IsCastrated"
                                        onChange={onInputchange}
                                    >
                                        {YesNo.map((item, index) => {
                                            return (
                                                <React.Fragment key={index}>
                                                    <option
                                                        value={index}
                                                        selected={
                                                            index ===
                                                            DogDetail.IsCastrated
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                </div>
                            </div>
                            <div>
                                <div className="form-group p-1">
                                    <label>Cena</label>
                                    <input
                                        type="number"
                                        className="form-control"
                                        name="Price"
                                        value={DogDetail.Price}
                                        onChange={onInputchange}
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

const EnumInShelterBcs = [
    "Neuvedeno",
    " Po zemřelém majiteli",
    "Nalezen",
    "Zabaven",
    "Narozen v útulku",
];
const EnumSex = ["Pes", "Fena"];
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
const YesNo = ["Ano", "Ne"];
