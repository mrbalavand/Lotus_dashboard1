import React, { useState, useEffect } from "react";
import axios from 'axios';
import AppContext from './AppContext';
import { useContext } from 'react';
import ReactLoading from 'react-loading';
import { DatePicker } from 'react-persian-datepicker';
import './FundTable.css'


export function FundTable() {

    let [activeunit, setactiveunit] = useState([]);

    let [activetablestartdate, setactivetablestartdate] = useState("");


    let [activetableenddate, setactivetableenddate] = useState("");

    let [sdate, setsdate] = useState("");
    let [edate, setedate] = useState("");


    let activestate6 = false;
    let myContext = useContext(AppContext);
    let rownum = 1;

    

    useEffect(() => {

        will1()


    }, [])


    async function will1() {

        let Parameters = [{ Name: "startdate", Value: "" }, { Name: "enddate", Value: "" }];
        const active = await axios.post(myContext.url + 'api/ActiveUnit', Parameters);
        console.log(active)
        activestate6 = true
        setactiveunit(active.data);
    }


    async function Activetablefilter() {

        let Parameters = [{ Name: "startdate", Value: sdate }, { Name: "enddate", Value: edate }];
        setactiveunit("")
        const active = await axios.post(myContext.url + 'api/ActiveUnit', Parameters);
        //console.log(active)
        //activestate6 = true
        setactiveunit(active.data);


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
    async function getvaluesdate(e) {

        console.log(e._d.toLocaleDateString('fa-IR'))
        if (e._d.toLocaleDateString('fa-IR') !== null) {
            
            let sdate1 =await e._d.toLocaleDateString('fa-IR').toString();
            
            setsdate(sdate1);

            console.log(sdate)

        }

    }


    async function getvalueedate(e) {

        console.log(e._d.toLocaleDateString('fa-IR'))
        if (e._d.toLocaleDateString('fa-IR') !== null) {
            let edate1 =await e._d.toLocaleDateString('fa-IR')
            setedate(edate1);
            
            console.log(edate)

        }

    }

    return (

        <div className="row d-flex justify-content-center align-item-center pt-3 me-5 ms-5 mt-4 shadow" style={{ borderRadius: "10px", backgroundColor: "rgb(250,250,250)", height: "400px" }}>


            {activeunit.length === 0 ? (<p className="col-md-2 pt-3"><ReactLoading type="spinningBubbles" color="rgb(0,50,60)" height={100} /></p>) :
                (

                    <div className="row">
                        {sdate.length !== 0 ? (<div className="container" id="configdate">تعداد واحد فعال در صندوقهای تامین سرمایه لوتوس (تفاضل صدور و ابطال در بازه {sdate} لغایت {edate})</div>)
                            : <div className="container" id="configdate">تعداد واحد فعال در صندوقهای تامین سرمایه لوتوس (تفاضل صدور و ابطال در یک ماه اخیر )</div>
                        }

                        <table className="table table-bordered table-responsive">


                            <thead>
                                <tr style={{ backgroundColor: "rgb(0,50,60)", color: "white" }}>
                                    <th scope="col">ردیف</th>
                                    <th scope="col">نام صندوق</th>
                                    <th scope="col">تعداد واحد </th>

                                </tr>
                            </thead>

                            <tbody>


                                {activeunit.map(item => (
                                    <tr>
                                        <td >{rownum++}</td>
                                        <td>{item.fundname}</td>
                                        <td>{commafy(item.fundlicense)}</td>



                                    </tr>


                                ))}

                            </tbody>


                        </table>


                        {/*<div className="row col-md-12 g-0">*/}

                        {/*    <div className="col-md-4">*/}
                        {/*        */}{/*<span className="input-group-text" id="basic-addon1">تاریخ شروع</span>*/}
                        {/*        <div className="col-md-4">تاریخ شروع</div><div className="col-md-8" style={{ display: "inline" }}><DatePicker value={activetablestartdate} onChange={(value) => { setactivetablestartdate(value) }} placeholder="تاریخ شروع" id="start1" /></div>*/}
                        {/*    </div>*/}

                        {/*    <div className="col-md-4">*/}
                        {/*        */}{/*<span className="input-group-text" id="basic-addon1">تاریخ پایان</span>*/}
                        {/*        <div className="col-md-4">تاریخ پایان</div><DatePicker value={activetableenddate} onChange={(value) => { setactivetableenddate(value) }} className="col-md-8" placeholder="تاریخ پایان" id="start2"/>*/}
                        {/*        */}{/* <input type="text" className="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1" />*/}

                        {/*    </div>*/}


                        {/*    <div className="col-md-4">*/}

                        {/*        <p className=""></p><button className=" btn btn-success" style={{ width: "100%", height: "30px", fontSize: "14px", backgroundColor: "rgb(0,60,50)" }} onClick={Activetablefilter}>فیلتر</button>*/}

                        {/*    </div>*/}
                        {/*</div>*/}




                       






                    </div>



                )

            }


            <div className="row g-0 d-flex justify-content-start align-items-center">


                {/*<span className="input-group-text" id="basic-addon1">تاریخ شروع</span>*/}


                <div className="col-md-3 ps-3">
                    {/*<span className="input-group-text" id="basic-addon1">تاریخ شروع</span>*/}
                    <p className="col-md-3 mt-1" style={{ float: "right",display:"inline" }}>تاریخ شروع</p><div className="col-md-9"><DatePicker value={activetablestartdate} onChange={(value) => { setactivetablestartdate(value) }} onChange={(e) => { getvaluesdate(e) }}/></div>
                </div>

                <div className="col-md-3 ps-3">
                    {/*<span className="input-group-text" id="basic-addon1">تاریخ پایان</span>*/}
                    <p className="col-md-3 mt-1" style={{ float: "right", display: "inline" }} >تاریخ پایان</p><div className="col-md-9"><DatePicker value={activetableenddate} onChange={(value) => { setactivetableenddate(value) }} onChange={(e) => { getvalueedate(e) }}/></div>
                    {/* <input type="text" className="form-control" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1" />*/}
                    
                </div>


                <div className="col-md-1">

                    <button className=" btn btn-success mb-2" style={{ width: "100%", height: "30px", fontSize: "14px", backgroundColor: "rgb(0,60,50)" }} onClick={Activetablefilter}>فیلتر</button>

                </div>

            </div>


        </div>

    )
}