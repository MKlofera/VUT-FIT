/**
 * author: Marek Klofera
 * login: xklofe01
 */

import { constrainPoint } from "@fullcalendar/react";
import React, { useState, useEffect } from "react";
import axios from "axios";

import { isUserGranted, isUserRole, UserRoles } from "../utils/UserUtils";

export default function DogCreationPage() {
    const [DogDetail, setDogDetail] = useState({
        Name: "",
        InShelterBcs: 0,
        Age: 0,
        Sex: 0,
        Breed: 0,
        ChipNumber: "0",
        Dewormed: 0,
        Vaccinated: 0,
        IsCastrated: 0,
        Price: 500,
        Description: "",
        Photo: "https://st2.depositphotos.com/2166845/5890/i/450/depositphotos_58906929-stock-photo-cairn-terrier-puppy.jpg",
    });
    const [DogIsValid, setDogIsValid] = useState({
        Name: 0,
        Age: 0,
        ChipNumber: 0,
        Price: 0,
        Description: 0,
    });
    const [DogSubmited, setDogSubmited] = useState(false);
    const [selectedFile, setSelectedFile] = useState(null);
    const [DogDetailToServer, setDogDetailToServer] = useState(null);

    const fileSelectedHandler = (e) => {
        const formData = new FormData();
        formData.append(
            "my-image-file",
            e.target.files[0],
            e.target.files[0].name
        );
        setSelectedFile(formData);
    };

    const fileUploadHandler = () => {
        axios.post("/API/dog/uploadImage", selectedFile).then((res) => {
        });
    };

    function onInputchange(event) {
        setDogDetail({
            ...DogDetail,
            [event.target.name]: event.target.value,
        });
    }
    const submitDogHandler = (e) => {
        setDogSubmited(true);
        if (inputsAreValid()) {
            setDogDetailToServer(DogDetail);
        }
    };

    const sendDogToServer = async (data) => {
        // setIsLoading(true);
        try {
            const response = await fetch(`/API/dog/new`, {
                method: "POST",
                body: JSON.stringify(data),
            });
        } catch (err) {
            console.log(err.message);
        }
        window.location = "/";
    };

    const inputsAreValid = () => {
        let NameIsValid = ValidName();
        let AgeIsValid = ValidAge();
        let ChipNumberIsValid = ValidChipNumber();
        let PriceIsValid = ValidPrice();
        let DescriptionIsValid = ValidDescription();

        setDogIsValid({
            Name: NameIsValid,
            Age: AgeIsValid,
            ChipNumber: ChipNumberIsValid,
            Price: PriceIsValid,
            Description: DescriptionIsValid,
        });
        if (
            NameIsValid &&
            AgeIsValid &&
            ChipNumberIsValid &&
            PriceIsValid &&
            DescriptionIsValid
        ) {
            return true;
        } else {
            return false;
        }
    };
    const ValidName = () => {
        if (DogDetail.Name.length < 2 || DogDetail.Name.length > 30) {
            return false;
        }
        return true;
    };
    const ValidAge = () => {
        if (DogDetail.Age < 0 || DogDetail.Age > 100) {
            return false;
        }
        return true;
    };
    const ValidChipNumber = () => {
        let isNum = /^\d+$/.test(DogDetail.ChipNumber);
        if (!isNum || DogDetail.ChipNumber.length !== 15) {
            return false;
        }
        return true;
    };
    const ValidPrice = () => {
        let isNum = /^\d+$/.test(DogDetail.ChipNumber);
        if (!isNum || DogDetail.Price < 0 || DogDetail.Price > 100000) {
            return false;
        }
        return true;
    };
    const ValidDescription = () => {
        if (
            DogDetail.Description.length < 20 ||
            DogDetail.Description.length > 2000
        ) {
            return false;
        }
        return true;
    };

    useEffect(() => {
        if (DogDetailToServer !== null) sendDogToServer(DogDetailToServer);
    }, [DogDetailToServer]);
    useEffect(() => {
        if (selectedFile !== null) fileUploadHandler(selectedFile);
    }, [selectedFile]);
    useEffect(() => {
    }, [DogIsValid]);


    if (
        !isUserRole(UserRoles.ROLE_ADMIN) &&
        !isUserRole(UserRoles.ROLE_SOCIAL_WORKER)
    ) {
        window.location = "/login";
    } else {
        return (
            <section className=" container DogDetailInfoComponent">
                <div className="row mt-3">
                    <div className="col-xl-4 col-lg-6 px-3">
                        {/* <img
                        src={
                            "https://www.hpnemec.cz/wp-content/themes/consultix/images/no-image-found-360x250.png"
                        }
                        alt="dog"
                        className="img-fluid"
                    />
                    <div className="m-3">
                        <input
                            className="form-control"
                            type="file"
                            id="formFile"
                            onChange={(e) => {
                                fileSelectedHandler(e);
                            }}
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
                                placeholder="Hafan"
                                onChange={onInputchange}
                            />
                            <small
                                style={
                                    DogSubmited
                                        ? DogIsValid.Name
                                            ? null
                                            : { color: "red" }
                                        : null
                                }
                            >
                                Jméno psa musí mít délku 1-30 znaků
                            </small>
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
                                            {index === 0 ? (
                                                <option
                                                    value={index}
                                                    selected={
                                                        index ===
                                                        DogDetail.InShelterBcs
                                                    }
                                                >
                                                    {item}
                                                </option>
                                            ) : (
                                                <option value={index}>
                                                    {item}
                                                </option>
                                            )}
                                        </React.Fragment>
                                    );
                                })}
                                ;
                            </select>
                            <small></small>

                        </div>
                        <div className="row">
                            <div className="col-6">
                                <div className="form-group p-1">
                                    <label>Věk</label>
                                    <input
                                        min={0}
                                        max={60}
                                        defaultValue={1}
                                        type="number"
                                        name="Age"
                                        className="form-control"
                                        onChange={onInputchange}
                                    />

                                    <small
                                        style={
                                            DogSubmited
                                                ? DogIsValid.Age
                                                    ? null
                                                    : { color: "red" }
                                                : null
                                        }
                                    >
                                        Věk může být pouze číslo od 0-40
                                    </small>
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
                                                            DogDetail.Sex
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                    <small></small>

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
                                                            DogDetail.Breed
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                    <small></small>
                                </div>
                                <div className="form-group p-1">
                                    <label>Číslo čipu</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        name="ChipNumber"
                                        placeholder="123456789123456"
                                        onChange={onInputchange}
                                    />
                                    <small
                                        style={
                                            DogSubmited
                                                ? DogIsValid.ChipNumber
                                                    ? null
                                                    : { color: "red" }
                                                : null
                                        }
                                    >
                                        Číslo čipu musí obsahovat 15 číslic 0-9
                                    </small>
                                </div>
                            </div>
                            <div className="col-6 rightColumnInfo d-flex flex-column justify-content-between">
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
                                                            index === 0
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                    <small></small>
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
                                                            index === 0
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                    <small></small>
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
                                                            DogDetail.Breed
                                                        }
                                                    >
                                                        {item}
                                                    </option>
                                                </React.Fragment>
                                            );
                                        })}
                                        ;
                                    </select>
                                    <small></small>
                                </div>
                                <div>
                                    <div className="form-group p-1">
                                        <label>Cena</label>
                                        <input
                                            min={0}
                                            defaultValue={500}
                                            type="number"
                                            className="form-control"
                                            name="Price"
                                            onChange={onInputchange}
                                        />

                                        <small
                                            style={
                                                DogSubmited
                                                    ? DogIsValid.Price
                                                        ? null
                                                        : { color: "red" }
                                                    : null
                                            }
                                        >
                                            Cena může být pouze číslo (0-100000)
                                        </small>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group p-1">
                        <label>Popis psa</label>
                        <textarea
                            className="form-control"
                            id="exampleFormControlTextarea1"
                            name="Description"
                            rows="3"
                            placeholder="Hafan je hodný pejsek ..."
                            onChange={onInputchange}
                        ></textarea>

                        <small
                            style={
                                DogSubmited
                                    ? DogIsValid.Description
                                        ? null
                                        : { color: "red" }
                                    : null
                            }
                        >
                            Popis psa musí obsahovat minimálně 20 znaků a
                            maximálně 2000 znaků
                        </small>
                    </div>
                </div>
                <div className="row">
                    <button
                        className="btn btn-success"
                        onClick={(e) => {
                            submitDogHandler(e);
                        }}
                    >
                        Uložit psa
                    </button>
                </div>
            </section>
        );
    }
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
