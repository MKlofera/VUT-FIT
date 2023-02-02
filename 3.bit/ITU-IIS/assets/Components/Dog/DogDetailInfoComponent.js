/**
 * author: Marek Klofera
 * login: xklofe01
 */
import React, {useState} from "react";
import { Link } from "react-router-dom";

// outsources
import ImageGallery from "react-image-gallery";

//Icons
import { MdDone } from "react-icons/md";
import { FcCancel } from "react-icons/fc";

// Components
import VetFilterComponent from "../Filters/VetFilterComponent";
import DogAddoptionModal from "./DogAddoptionModal";

export default function DogDetailInfoComponent(props) {
    const [AddoptDogModalVisible, setAddoptDogModalVisible] = useState(false);

    return (
        <section className="DogDetailInfoComponent">
            <div className="row mt-3">
                <div className="col-xl-4 col-lg-6 px-3">
                    <img
                        src={props.DogDetail.Photo}
                        alt="dog"
                        className="img-fluid"
                    />
                    {/* <ImageGallery items={images} /> TODO consider more than one image*/}
                </div>
                <div className="col-xl-8 col-lg-6 px-3">
                    <h1>{props.DogDetail.Name}</h1>
                    <p className="mb-4">
                        {EnumInShelterBcs[props.DogDetail.InShelterBcs]}
                    </p>
                    <div className="row">
                        <div className="col-6">
                            <p>Věk: {props.DogDetail.Age}</p>
                            <p>
                                Pohlaví: {props.DogDetail.Sex ? "Pes" : "Fena"}
                            </p>
                            <p>Plemeno: {EnumBreed[props.DogDetail.Breed]}</p>
                            <p>číslo čipu: {props.DogDetail.ChipNumber}</p>
                        </div>
                        <div className="col-6 rightColumnInfo d-flex flex-column justify-content-between">
                            <div>
                                <span>
                                    Odčervení:{" "}
                                    {props.DogDetail.Dewormed ? (
                                        <FcCancel />
                                    ) : (
                                        <MdDone />
                                    )}
                                </span>{" "}
                                <br />
                                <span>
                                    Očkování:{" "}
                                    {props.DogDetail.Vaccinated ? (
                                        <FcCancel />
                                    ) : (
                                        <MdDone />
                                    )}
                                </span>
                                <br />
                                <span>
                                    Kastrace:{" "}
                                    {props.DogDetail.Castration ? (
                                        <FcCancel />
                                    ) : (
                                        <MdDone />
                                    )}
                                </span>
                                <br />
                            </div>
                            <div>
                                <p className="price mt-5">{`${props.DogDetail.Price},- Kč`}</p>
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="p-2 col-xl-6 col-lg-6 col-md-6 col-sm-12">
                            <button
                                className="button btn-interested"
                                onClick={() => {
                                    setAddoptDogModalVisible(true);
                                }}
                            >
                                Adoptovat psa
                            </button>
                            <DogAddoptionModal
                                show={AddoptDogModalVisible}
                                onHide={() => setAddoptDogModalVisible(false)}
                            />
                        </div>
                        <div className="p-2 col-xl-6 col-lg-6 col-md-6 col-sm-12">
                            <Link to={`/calendar/${props.DogDetail.id}`}>
                                <button className="button btn-walking">
                                    Naplánovat venčení
                                </button>
                            </Link>
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
