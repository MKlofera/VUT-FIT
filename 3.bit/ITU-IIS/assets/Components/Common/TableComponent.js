/**
 * @Author: Marek Klofera
 * @Created: 19.11.2022
 */

 import { elementType } from "prop-types";
 import React, { useEffect } from "react";
 import { useState } from "react";
 import { Link } from "react-router-dom";
 
 /**
  *
  * @param {props.Header} array of strings to fill the table header
  * @param {props.Body} array of objects to fill the table body
  * @param {props.Links} array of link defining if the table cell should be a link and link path
  *
  * @param {props.SendToParentSelectedRows} function to send selected rows (id's) to parent component
  * @returns
  *
  * props.Header = ["cell-0", "cell-1", "cell-2", "cell-3", "cell-4"]
  * props.Body = [
  *    {
  *      id: "1",                // first should be always id!!!
  *      cell-0: "cell-0",
  *      cell-1: "cell-1",
  *      cell-2: "cell-2",
  *    },
  *    {
  *      id: "2",                // first should be always id!!!
  *      cell-0: "cell-0",
  *      cell-1: "cell-1",
  *      cell-2: "cell-2",
  *    }, ...
  *  ]
  */
 
 export default function TableWithCheckboxesComponent(props) {
     const [TableHeader, setTableHeader] = useState([]);
     const [TableBody, setTableBody] = useState([]); // deserialize json to array
 
     const [selectedRows, setSelectedRows] = useState([]);
     const [allCheckboxesSelected, setAllCheckboxesSelected] = useState(false);
 
     const [RemoveCheckboxes, setRemoveCheckboxes] = useState(false);
 
     const handleRowCheckBoxChange = (rowId) => {
         rowId = parseInt(rowId);
         if (selectedRows.includes(rowId)) {
             setSelectedRows(selectedRows.filter((id) => id !== rowId));
         } else {
             setSelectedRows([...selectedRows, rowId]);
         }
     };
 
     const handleAllCheckBoxChange = () => {
         if (allCheckboxesSelected) {
             setSelectedRows([]);
         } else {
             let allRows = [];
             TableBody.forEach((element) => {
                 allRows.push(element[0]);
             });
 
             setSelectedRows(allRows);
         }
         setAllCheckboxesSelected(!allCheckboxesSelected);
     };
 
     const handleCheckBoxChecked = (rowId) => {
         let checked = false;
         if (selectedRows.includes(rowId)) {
             checked = true;
         }
         return checked;
     };
 
     useEffect(() => {
         props.SendToParentSelectedRows(selectedRows);
     }, [selectedRows]);
     useEffect(() => {}, [allCheckboxesSelected]);
     useEffect(() => {
         setSelectedRows([]);
     }, [RemoveCheckboxes]);
     useEffect(() => {
         ParseJson();
     }, []);
 
     return (
         <div>
             <table className="TableComponent table table-striped table-responsive-sm">
                 <thead>
                     <tr>
                         <th scope="col">
                             <div className="form-check">
                                 <input
                                     className="form-check-input"
                                     type="checkbox"
                                     value=""
                                     id="flexCheckDefault"
                                     onClick={(e) => {
                                         handleAllCheckBoxChange(
                                             allCheckboxesSelected
                                         );
                                     }}
                                 />
                             </div>
                         </th>
                         {TableHeader.map((element, index) => (
                             <th scope="col" key={index}>
                                 {element}
                             </th>
                         ))}
                     </tr>
                 </thead>
                 <tbody>
                     {props.children}
                 </tbody>
             </table>
         </div>
     );
 }
 