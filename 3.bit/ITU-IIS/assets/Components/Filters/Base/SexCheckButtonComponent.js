/**
 * author: Marek Klofera 
 * login: xklofe01
 */

import React from "react";
import { useState, useEffect } from "react";
import { Button } from "react-bootstrap";

// functional version

export default function SexCheckButtonComponent({ handleSex }) {
    const [MalePicked, SetMalePicked] = useState(false);
    const [FemalePicked, SetFemalePicked] = useState(false);

    function ChangeMale() {
        SetMalePicked(!MalePicked);
    }
    function ChangeFemale() {
        SetFemalePicked(!FemalePicked);
    }

    useEffect(() => {
        if ((MalePicked && FemalePicked) || (!MalePicked && !FemalePicked)) {
            handleSex("both");
        } else if (MalePicked && !FemalePicked) {
            handleSex("male");
        } else if (!MalePicked && FemalePicked) {
            handleSex("female");
        }
    }, [MalePicked, FemalePicked]);

    return (
        <div className="row SexCheckButtonComponent">
            <div className="col-md-6 col-sm-6">
                <button
                    onClick={() => {
                        ChangeMale();
                    }}
                    className={`button ${MalePicked ? "active" : ""}`}
                    type="button"
                >
                    Pes
                </button>
            </div>

            <div className="col-md-6 col-sm-6">
                <button
                    onClick={() => {
                        ChangeFemale();
                    }}
                    className={`button ${FemalePicked ? "active" : ""}`}
                    type="button"
                >
                    Fena
                </button>
            </div>
        </div>
    );
}
