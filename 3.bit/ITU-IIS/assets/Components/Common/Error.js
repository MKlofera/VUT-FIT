/**
 * author: Marek Klofera 
 * login: xklofe01
 */
import React from "react";

export default function Error(props) {
    return (
        <div className="alert alert-danger" role="alert">
            {props.Error}
        </div>
    );
}
