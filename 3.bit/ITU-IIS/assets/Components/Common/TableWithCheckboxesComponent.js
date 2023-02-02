/**
 * @Author: Marek Klofera
 * login: xklofe01
 */

import {elementType} from "prop-types";
import React, {useEffect} from "react";
import {useState} from "react";
import {Link} from "react-router-dom";

/**
 *
 * @param {props.Header} array of strings to fill the table header
 * @param {props.Body} array of objects to fill the table body
 * @param {props.Links} array of link defining if the table cell should be a link and link path
 *
 * @param {props.SendToParentSelectedRows} function to send selected rows (id's) to parent component
 * @param {props.ShowCheckBoxes} bool if true, table will show checkboxes on each row
 * @returns
 *
 * props.Header = ["cell-0", "cell-1", "cell-2", "cell-3", "cell-4"]
 * props.Body = [
 *    {
 *      id: "1",
 *      cell-0: "cell-0",
 *      cell-1: {id: "1", text: "cell-1"},      // will be rendered as a link to /cell-1/id
 *      cell-2: "cell-2",
 *    },
 *    {
 *      id: "2",
 *      cell-0: "cell-0",
 *      cell-1: {id: "2", text: "cell-1"},      // will be rendered as a link to /cell-1/id
 *      cell-2: "cell-2",
 *    }, ...
 *  ]
 */

export default function TableWithCheckboxesComponent(props) {
    // empty table body
    if (props.Body === undefined || props.Body === null || props.Body.length === 0) {
        return (
            <div>
                <table className="TableComponent table table-striped table-responsive-sm">
                    <thead>
                    <tr>
                        {props.Header.map((element, index) => (
                            <th scope="col" key={index}>
                                {element}
                            </th>
                        ))}
                    </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        );
    }

    const [TableBody, setTableBody] = useState(props.Body); // deserialize json to array
    const [selectedRows, setSelectedRows] = useState([]);
    const [allCheckboxesSelected, setAllCheckboxesSelected] = useState(false);
    const [RemoveCheckboxes, setRemoveCheckboxes] = useState(false);
    const [showCheckboxes, setShowCheckboxes] = useState(true);
    const [SearchingInput, SetSearchingInput] = useState(null);
    const [IsFilteredBySearchingInput, SetIsFilteredBySearchingInput] = useState(false);
    const [isSorted, setIsSorted] = useState({
        Sorted: false,
        column: null,
    });


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
            TableBody.forEach((element, index) => {
                Object.entries(element).map(([key, value], index) => {
                    if (key === "id") {
                        allRows.push(value);
                    }
                });
            });
            setSelectedRows(allRows);
        }
        setAllCheckboxesSelected(!allCheckboxesSelected);
    };

    const handleCheckBoxChecked = (rowId) => {
        if (selectedRows.includes(rowId)) {
            return true;
        }
        return false;
    };

    const GetValueToSort = (wholeRow, index) => {
        if (typeof Object.entries(wholeRow)[index][1] === "object") {
            let object = Object.entries(wholeRow)[index][1]
            return Object.entries(object)[1][1];
        } else {
            return Object.entries(wholeRow)[index][1];
        }
    }
    const SortAscending = (index) => {
        return TableBody.sort((a, b) => {
            let aVal = GetValueToSort(a, index);
            let bVal = GetValueToSort(b, index);
            if (aVal < bVal) {
                return -1;
            }
            if (aVal > bVal) {
                return 1;
            }
            return 0;
        });
    }
    const SortTable = (column) => {
        let index = column + 1;
        let sortedTable;
        if (isSorted.Sorted === false) {
            sortedTable = SortAscending(index);
            setIsSorted({
                Sorted: true,
                column: column
            });
        } else if (isSorted.Sorted === true) {
            sortedTable = SortAscending(index).reverse();
            setIsSorted({
                Sorted: false,
                column: column
            });
        }
        setTableBody(sortedTable);
    }

    const EmptyInput_ShowAllRows = (input) => {
        if (input === null || input === "" || input === undefined) {
            setTableBody(props.Body);
            return true;
        }
        return false
    }
    const FilterValueIncludesInput = (value, input) => {
        return String(value).toLowerCase().includes(input);
    }
    const FilterByInput = (input) => {
        if(EmptyInput_ShowAllRows(input)) return;

        input = input.toLowerCase();
        let filteredTable = [];
        props.Body.forEach((row) => {
            Object.entries(row).map(([key, value], index) => {
                if (typeof value === "object") {
                    value = Object.entries(value)[1][1];
                    if (FilterValueIncludesInput(value, input)) {
                        if(!filteredTable.includes(row)){
                            filteredTable.push(row);
                        }
                    }
                } else if (FilterValueIncludesInput(value, input)) {
                    if(!filteredTable.includes(row)){
                        filteredTable.push(row);
                    }
                }
            });
            setTableBody(filteredTable);
        });

    }

    useEffect(() => {
        if (props.ShowCheckBoxes !== undefined && props.ShowCheckBoxes !== null) {
            setShowCheckboxes(props.ShowCheckBoxes);
        }
    }, []);
    useEffect(() => {
    }, [TableBody]);
    useEffect(() => {
        props.SendToParentSelectedRows(selectedRows);
    }, [selectedRows]);
    useEffect(() => {
    }, [allCheckboxesSelected]);
    useEffect(() => {
        setSelectedRows([]);
    }, [RemoveCheckboxes]);


    return (
        <div className="TableComponent">
            <div className="d-flex justify-content-end my-2">
                <input className="form-control searchInput" type="text" onChange={(e) => {
                    FilterByInput(e.target.value)
                }}/>
                <button className="btn btn-primary" >Search</button>
            </div>
            <table className="table table-striped table-responsive-sm">
                <thead>
                <tr>
                    <th scope="col">
                        <div className="form-check">
                            {showCheckboxes ?
                                <input className="form-check-input" type="checkbox" value="" id="flexCheckDefault"
                                       onClick={(e) => {
                                           handleAllCheckBoxChange(allCheckboxesSelected);
                                       }}
                                /> : null}
                        </div>
                    </th>
                    {props.Header.map((element, index) => (
                        <th scope="col" key={index} className="TableHeader" onClick={() => {
                            SortTable(index)
                        }}>
                            {element}
                        </th>
                    ))}
                </tr>
                </thead>
                <tbody>
                {TableBody.map((row, index) => (
                    <tr key={index}>
                        <td scope="row">
                            <div className="form-check">
                                {showCheckboxes ?
                                    <input className="form-check-input" type="checkbox" value={parseInt(row.id)}
                                           id="flexCheckDefault"
                                           onChange={(e) => {
                                               handleRowCheckBoxChange(
                                                   e.target.value
                                               );
                                           }}
                                           checked={handleCheckBoxChecked(
                                               parseInt(row.id)
                                           )}
                                    /> : null}
                            </div>
                        </td>
                        {Object.entries(row).map(([key, value], index) => {
                            if (key !== "id") {
                                if (typeof value === "object") {
                                    return (
                                        <td key={index}>
                                            <Link to={`/${key}/${value.id}`}>{value.text}</Link>
                                        </td>
                                    );
                                }
                                return <td key={index}>{value}</td>;
                            }
                        })}
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
}
