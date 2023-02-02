/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import { element } from "prop-types";
import React from "react";

//Components
import DogListCardComponent from "./DogListCardComponent";

export default function DogListComponent(props) {
    return (
        <div className="row justify-content-center">
            {props.DogList.map((element, index) => {
                return (
                    <React.Fragment key={index}>
                        <DogListCardComponent
                            id={element.id}
                            name={element.Name}
                            photo={element.Photo}
                            breed={element.Breed}
                            age={element.Age}
                            sex={element.Sex}
                            price={element.Price}
                        />
                    </React.Fragment>
                );
            })}
        </div>
    );
}
