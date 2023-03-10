/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React, { useEffect, useState } from "react";
import Loading from "../Components/Common/Loading";

// components
import DogListCardComponent from "../Components/Dog/DogListCardComponent";
import DogListComponent from "../Components/Dog/DogListComponent";
import DogListFilterComponent from "../Components/Filters/DogListFilterComponent";

const MainPage = () => {
    const [IsLoading, setIsLoading] = useState(true);
    const [Error, setError] = useState(null);
    const [Filters_set, setFilters_set] = useState(false);
    const [Filters, SetFilters] = useState({
        Breed: null,
        MinAge: 1,
        MaxAge: 20,
        MinPrice: 500,
        MaxPrice: 3500,
        Sex: "both",
        FormInput: null,
    });
    const [DogList, SetDogList] = useState([]);
    const getData = async (url, method_type, body_content) => {
        try {
            let response = "";
            if (method_type == "GET") {
                response = await fetch(
                    url
                )
            }
            
            else {
                response = await fetch(
                    url,
                    {
                        method: method_type,
                        body: body_content,
                    })
            }
            if (!response.ok) {
                throw new Error(
                    `This is an HTTP error: The status is ${response.status}`
                );
            }
            let actualData = await response.json();
            SetDogList(actualData);
            setError(null);
        } catch (err) {
            setError(err.message);
            SetDogList(null);
        } finally {
            setIsLoading(false);
        }
    };

    const getFilters = (
        breed,
        minAge,
        maxAge,
        minPrice,
        maxPrice,
        sex,
        formInput
    ) => {
        SetFilters({
            Breed: breed,
            MinAge: minAge,
            MaxAge: maxAge,
            MinPrice: minPrice,
            MaxPrice: maxPrice,
            Sex: sex,
            FormInput: formInput,
        });
        setFilters_set(true);
        setIsLoading(true);
    };

    useEffect(() => { if (Filters_set) getData("/API/dog/filtered", "POST", JSON.stringify(Filters)) }, [Filters, Filters_set]);
    useEffect(() => {
        getData("/API/dog", "GET", "");
    }, []);

    return (
        <section className="container mainPage">
            <div className="d-flex justify-content-between my-3 flex-wrap-reverse">
                <div className="col-xl-6 col-lg-6 col-md-6 col-sm-12 my-3">
                    <div className="d-flex flex-column">
                        <h1>Sta??te se ????astn??m majitelem psa</h1>
                        <p>
                            V na??em ??tulku se nach??z?? mnoho pejsk??, kte???? ??ekaj??
                            na nov?? domovy. V??ichni jsou zdrav??, ??ist?? a
                            p??ipraveni na nov?? ??ivot. V??ce informac?? o
                            jednotliv??ch pejsc??ch naleznete v jejich kart??ch.
                        </p>
                    </div>
                </div>
                <div className="col-xl-6 col-lg-6 col-md-6 col-sm-12 my-3">
                    <img
                        className="topImage"
                        src="https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/dog-puppy-on-garden-royalty-free-image-1586966191.jpg?crop=1.00xw:0.669xh;0,0.190xh&resize=640:*"
                    />
                </div>
            </div>
            <DogListFilterComponent
                Selector={FilterSelector}
                getFilters={getFilters}
            ></DogListFilterComponent>
            {IsLoading ? <Loading /> : ""}
            {Error ? <Error Error={Error} /> : ""}
            {DogList ? <DogListComponent DogList={DogList} /> : ""}
        </section>
    );
};
export default MainPage;

const FilterSelector = [
    {
        name: "Afg??nsk?? chrt",
        breed: 0,
    },
    {
        name: "Anglick?? buldok",
        breed: 1,
    },
    {
        name: "Argentinsk?? doga",
        breed: 2,
    },
    {
        name: "Belgick?? ov????k",
        breed: 3,
    },
    {
        name: "B??gl",
        breed: 4,
    },
    {
        name: "Border kolie",
        breed: 5,
    },
    {
        name: "Bulteri??r",
        breed: 6,
    },
    {
        name: "??eskoslovensk?? vl????k",
        breed: 7,
    },
    {
        name: "Dalmatin",
        breed: 8,
    },
    {
        name: "Dobrman",
        breed: 9,
    },
    {
        name: "Holandsk?? ov????k",
        breed: 10,
    },
    {
        name: "Jezev????k",
        breed: 11,
    },
    {
        name: "N??meck?? ov????k",
        breed: 12,
    },
    {
        name: "Shiba-Inu",
        breed: 13,
    },
    {
        name: "Zlat?? retriever",
        breed: 14,
    }
];
