import React, { useState, useEffect } from "react";
import { Slider } from "./Slider";
import { NavMenu } from "../components/NavMenu";
import { Footer } from "../components/Footer";
import axios from "axios";
import Dropdown from 'react-bootstrap/Dropdown';
import { useContext } from 'react';
import AppContext from './AppContext';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoneyBill, faCommentDollar, faDonate, faCreditCard, faLineChart, faBalanceScale } from '@fortawesome/free-solid-svg-icons';
import ReactLoading from 'react-loading';
import { Treemap, Label } from 'recharts';
import Skeleton from 'react-loading-skeleton';
import { BarChart, Bar, CartesianGrid, XAxis, YAxis, Legend, Tooltip, ResponsiveContainer, Brush } from 'recharts';

export function Selectfunds() {


    let [fundname, setfundname] = useState("صندوق لوتوس");
    let [fundname1, setfundname1] = useState("صندوق لوتوس");
    let [funddata, setfunddata] = useState("");
    let [onlinedata, setonlinedata] = useState("");
    let activestate5 = false;
    let activestate3 = false;
   
    
    async function setfund(e) {


        if (e !== "لطفا یک صندوق را انتخاب نمایید") {
            setfundname(e)
            setfundname1(e)
            let costs = await axios.get(myContext.url + `api/OnlineOrder?fundname=${e}`)
            setfunddata(costs.data)
            do5()

            console.log(funddata.sodooramount)
        }

        console.log(fundname)

    }



    

    function commafy(num) {

        try {
            var str = num.toString().split('.');
            if (str[0].length >= 5) {
                str[0] = str[0].replace(/(\d)(?=(\d{3})+$)/g, '$1,');
            }
            if (str[1] && str[1].length >= 5) {
                str[1] = str[1].replace(/(\d{3})/g, '$1 ');
            }
            return str.join('.');
        } catch (e) {
            return 0
        }

    }



    const myContext = useContext(AppContext);



    useEffect(() => {


        do5()

        setfund("صندوق لوتوس")


    }, [])

    async function do5() {
        const chartdata5 = await axios.get(myContext.url + 'api/OnlineOrder?fundname=All_Funds');
        activestate5 = true;

        setonlinedata(chartdata5.data);
    }




    

    return (

        <div>

            <div className="row col-md-12 justify-content-center pb-3 ps-5" style={{ fontSize: "18px", fontWeight: "bold" }}>
                <p style={{ borderBottom: "1px solid", paddingBottom: "4px" }} className="col-md-3"><FontAwesomeIcon icon={faBalanceScale} size="2x" color="rgb(0,60,50)" /> داشبورد آنلاین روز جاری {onlinedata.today}</p>
            </div>



            <label className="mb-2 me-5 ms-5">آمار صدور و ابطال به صورت تجمعی در کلیه صندوق های سرمایه گذاری</label>
            <hr className="me-5 ms-5"></hr>
            <div className="row d-flex justify-content-around pt-0 pe-4 ps-4">

                <div className="col-md-3 d-flex justify-content-center align-items-center shadow text-white border" style={{ width: "300px", height: "150px", backgroundColor: "rgb(72,122,123)", borderRadius: "25px" }}>
                    <p className="d-block" style={{ display: "block", textAlign: "right" }}><FontAwesomeIcon icon={faCommentDollar} size="4x" className="ps-1" color="rgb(255,181,0)" /><br className="mb-2" />

                        {onlinedata.length === 0 ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="white" /></p>) : (<p>مجموع واحد صدور: {commafy(onlinedata.sodooramount)}</p>)}

                    </p>

                </div>

                <div className="col-md-3 d-flex justify-content-center align-items-center shadow text-white border" style={{ width: "300px", height: "150px", backgroundColor: "rgb(159,125,35)", borderRadius: "25px" }}>
                    <p className="d-block" style={{ display: "block", textAlign: "right" }}><FontAwesomeIcon icon={faMoneyBill} size="4x" className="ps-1" color="rgb(255,181,0)" /><br className="mb-2" />

                        {onlinedata.length !== 0 ? (<p>مجموع واحد ابطال: {commafy(onlinedata.ebtalamount)}</p>) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="white" /></p>)}
                    </p>
                </div>


                <div className="col-md-3 d-flex justify-content-center align-items-center shadow text-white border" style={{ width: "300px", height: "150px", backgroundColor: "rgb(0,104,94)", borderRadius: "25px" }}>
                    <p className="d-block" style={{ display: "block", textAlign: "right" }}><FontAwesomeIcon icon={faDonate} size="4x" className="ps-1" color="rgb(255,181,0)" /><br className="mb-2" />


                        {onlinedata.length !== 0 ? (<p>مجموع مبلغ صدور - ریال: {commafy(onlinedata.sodoorunit)}</p>) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="white" /></p>)}
                    </p>
                </div>

                <div className="col-md-3 d-flex justify-content-center align-items-center shadow text-white border" style={{ width: "300px", height: "150px", backgroundColor: "rgb(111,162,135)", borderRadius: "25px" }}>
                    <p className="d-block" style={{ display: "block", textAlign: "right" }}><FontAwesomeIcon icon={faCreditCard} size="4x" className="ps-1" color="rgb(255,181,0)" /><br className="mb-2" />

                        {onlinedata.length !== 0 ? (<p>مجموع مبلغ ابطال - ریال: {commafy(onlinedata.ebtalunit)}</p>) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="white" /></p>)}



                    </p>
                </div>





            </div>



            <div className="d-flex justify-content-center  align-items-center me-5 ms-5 mt-5 mb-5">
                <div className="row w-100">

                    <div className="col-md-12 mb-3">
                        <div className="col-md-12">
                            <label className=" mb-2">آمار صدور و ابطال به تفکیک صندوق ها</label>
                            <hr></hr>
                            <Dropdown>
                                <Dropdown.Toggle variant="success" id="dropdown-basic" className="shadow text-light mb-5" style={{ backgroundColor: "rgb(72,122,123)", alignItems: "center" }}>
                                    {fundname}
                                </Dropdown.Toggle>

                                <Dropdown.Menu>
                                    
                                    <Dropdown.Item onClick={(e) => { setfund('صندوق لوتوس') }}>صندوق لوتوس</Dropdown.Item>
                                    <Dropdown.Item onClick={(e) => { setfund('صندوق پیروزان') }}>صندوق پیروزان</Dropdown.Item>
                                    <Dropdown.Item onClick={(e) => { setfund('صندوق زرین') }}>صندوق زرین</Dropdown.Item>
                                    <Dropdown.Item onClick={(e) => { setfund('صندوق رویان') }}>صندوق رویان</Dropdown.Item>
                                    <Dropdown.Item onClick={(e) => { setfund('صندوق الزهرا') }}>صندوق الزهرا</Dropdown.Item>
                                </Dropdown.Menu>
                            </Dropdown>
                        </div>
                    </div>


                    <div className="col-md-3">
                        <div className="card">
                            <div className="card-header">
                                <FontAwesomeIcon icon={faCommentDollar} size="2x" className="ps-0" color="rgb(255,181,0)" /> مجموع تعداد صدور {fundname1}
                            </div>
                            <div className="card-body shadow  text-center" style={{ height: "100px", fontSize: "18px" }}>
                                <p className="card-text">{funddata.length !== 0 ? (commafy(funddata.sodooramount)) : (0)}</p>
                            </div>
                        </div>
                    </div>

                    <div className="col-md-3">
                        <div className="card">
                            <div className="card-header">
                                <FontAwesomeIcon icon={faCommentDollar} size="2x" className="ps-0" color="rgb(255,181,0)" /> مجموع تعداد ابطال {fundname1}
                            </div>
                            <div className="card-body shadow text-center" style={{ height: "100px", fontSize: "18px" }}>
                                <p className="card-text">{funddata.length !== 0 ? (commafy(funddata.ebtalamount)) : (0)}</p>
                            </div>
                        </div>
                    </div>


                    <div className="col-md-3">
                        <div className="card" >
                            <div className="card-header">
                                <FontAwesomeIcon icon={faMoneyBill} size="2x" className="ps-0" color="rgb(255,181,0)" /> مجموع مبلغ صدور {fundname1}-ریال
                            </div>
                            <div className="card-body shadow text-center" style={{ height: "100px", fontSize: "18px" }}>
                                <p className="card-text">{funddata.length !== 0 ? (commafy(funddata.sodoorunit)) : (0)}</p>
                            </div>
                        </div>
                    </div>


                    <div className="col-md-3">
                        <div className="card">
                            <div className="card-header" >
                                <FontAwesomeIcon icon={faDonate} size="2x" className="ps-1" color="rgb(255,181,0)" /> مجموع مبلغ ابطال {fundname1}-ریال
                            </div>
                            <div className="card-body shadow text-center" style={{ height: "100px", fontSize: "18px" }}>
                                <p className="card-text">{funddata.length !== 0 ? (commafy(funddata.ebtalunit)) : (0)}</p>
                            </div>
                        </div>
                    </div>
                </div>




            </div >



            




        </div>
    )

}