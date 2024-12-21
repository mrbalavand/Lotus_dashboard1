import React from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faDashboard, faFileExcel, faBarChart, faTable } from '@fortawesome/free-solid-svg-icons';

export function Slider() {



    return (
        <div className="row d-flex p-0 m-0" style={{ height: "100%" }}>
            <div className="backgruondblack shadow rounded" id="slider" style={{height: "100%" }}>
                <ul className="nav flex-column p-0">


                    <li className="nav-item">
                        <a className="nav-link active text-white mt-1 mb-2" href="#"><FontAwesomeIcon icon={faDashboard} style={{ color:"rgb(199,201,199)"}} /> منو اصلی</a>

                    </li>
                    <li className="nav-item">
                        <a className="nav-link text-white mb-2" href="#"> <FontAwesomeIcon icon={faFileExcel} /> گزارشات اکسلی</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link text-white mb-2" href="#"><FontAwesomeIcon icon={faBarChart} /> سایر نمودارها</a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link text-white mb-2" href="#"><FontAwesomeIcon icon={faTable} /> جداول</a>
                    </li>
                </ul>


            </div>
        </div>

    )



}