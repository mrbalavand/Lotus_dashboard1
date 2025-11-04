import React, { useState, useEffect } from "react";
import { Slider } from "./Slider";
import { NavMenu } from "./NavMenu";

import { Footer } from "./Footer";
import axios from "axios";
import "../../src/Highcharts.css";
/*import Highcharts from "highcharts";*/
import { BarChart, Bar, CartesianGrid, XAxis, YAxis, Legend, Tooltip, ResponsiveContainer, Brush } from 'recharts';
import { Line, LineChart } from 'recharts';

import ReactLoading from 'react-loading';

import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import AppContext from './AppContext';
import './DatePicker.css';
import './Onlinedash.css';

import { DatePicker } from 'react-persian-datepicker';
//import FusionCharts from "fusioncharts";
//import charts from "fusioncharts/fusioncharts.charts";
//import ReactFusioncharts from "react-fusioncharts";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoneyBill, faCommentDollar, faDonate, faCreditCard, faLineChart, faBalanceScale } from '@fortawesome/free-solid-svg-icons';

import Dropdown from 'react-bootstrap/Dropdown';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
/*import GaugeChart from 'react-gauge-chart'*/
import { faUser, faPowerOff, faFaceLaugh, faTable, faBars, faPieChart, faColumns, faList, faUserCircle, faPeopleGroup, faUserGroup, faSpider, faDashboard, faCartArrowDown, faSquare, faCheckCircle, faAdd, faCheck } from "../../../../node_modules/@fortawesome/free-solid-svg-icons/index";
import TableScrollbar from 'react-table-scrollbar';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { Pie, PieChart, Cell } from 'recharts';
import { CountTime } from './CountTime';

import { useSelector, useDispatch } from 'react-redux';
import { GoldEtemadSlice } from '../ReduxApp/Gold_Etemad_Slice.js';
import { Datepicker as Datepicker1 } from '@ijavad805/react-datepicker';
import { Maxunitmodal } from "./Maxunitmodal";



let tablecount = 0;

let activestate = false;


let data1 = [];
let activestate4 = false;
let activestate5 = false;



let onlinedatanloading = false;


let scrolltableloading = false;

let sodoordash = false;

let registeruser = false;
let activestate7 = false;
let maxunitloading = false;
let onlineordercheck = false;
let setcolor = "rgb(255,255,255)"
let setcolor1 = "rgb(255,255,255)"
let setcolor2 = "rgb(255,255,255)"
let checkloginn = false;
let maxunitloading1 = false;
let maxunitloading2 = false;
let maxnavdate = false;
let checkaum = false;



/*commafy((branch[0].sodoorAmount) / 10)*/


export function Onlinedashboard() {
    const dispatch = useDispatch();
    const Gold_Etemad = useSelector(state => state.Gold_Etemad);


    console.log(Gold_Etemad)

    let tokendata = [{ token1: "" }];
    let [sodoor, setsodoor] = useState([]);


    let [checklogin, setchecklogin] = useState("");
    let [token, settoken] = useState([]);
    let history = useNavigate();
    let [linechartstartdate, setlinechartstartdate] = useState("");
    let [linechartenddate, setlinechartenddate] = useState("");
    let [barchartstartdate, setbarchartstartdate] = useState("");
    let [barchartenddate, setbarchartenddate] = useState("");

    let [onlineordershour, setonlineordershour] = useState([]);

    let [fundname, setfundname] = useState("صندوق لوتوس");
    let [fundname1, setfundname1] = useState("صندوق لوتوس");
    let [funddata, setfunddata] = useState("");
    let [onlinedata, setonlinedata] = useState("");
    let [onlinedata1, setonlinedata1] = useState("");
    let [piedata, setpiedata] = useState([{ name: "صدور", value: 0 }, { name: " ابطال", value: 0 }]);
    let [onlinedatan, setonlinedatan] = useState("");
    let [mostonline, setmostonline] = useState("");
    let [onlineorders, setonlineorders] = useState("");
    let [regnum, setregnum] = useState("");
    let [fundvalue, setfundvalue] = useState("");
    let [fundcapital, setfundcapital] = useState("");
    let [time, settime] = useState(0);
    let [branch, setbranch] = useState("");
    let [branchkeshavarzi, setbranchkeshavarzi] = useState("");
    /*let [subbranchkeshavarzi, subsetbranchkeshavarzi] = useState("");*/
    let [subbranch, setsubbranch] = useState("");
    let [mobile, setmobile] = useState("");

    let [maxunit, setmaxunit] = useState("");
    let [maxunit1, setmaxunit1] = useState("");
    let [navdate, setnavdate] = useState("");
    let [opentable, setopentable] = useState("");
    let [opentable1, setopentable1] = useState("");
    let [userinfo, setuserinfo] = useState("");
    let [showallbranches, setshowallbranches] = useState("");

    let [AUM, setAUM] = useState("");


    const [time1, setTime1] = useState(new Date());

    const [ptime, setptime] = useState(new Date().toLocaleDateString());

    let branchesval = false;
    let branchesvalkeshavarzi = false;
    let branchesval1 = false;
    let branchesval1keshavarzi = false;
    let mobileval = false;


    let accessuser = ["آقای جمال الدین نیک اندیش", "آقای هادی روزگار", "علیرضا بالاوند", "ceo", "خانم مریم خوشدل"]
    //useEffect(() => {

    //    /* setfund("صندوق لوتوس")*/

    //    const interval = setInterval(() => {
    //        setTime((time) => time - 1)
    //        /*setfund("صندوق لوتوس")*/
    //    }, 1000)


    //    return () => clearInterval(interval)

    //}, [])




    useEffect(() => {

        setTime1(new Date());



    }, []);




    let activestate6 = false;
    let activestate3 = false;

    let chartdata7 = "";
    let data = [];
    let rownum = 1;
    let rownum1 = 1;




    //useEffect(() => {

    //    let Token = localStorage.getItem("lotus_dashboard");


    //    tokendata.token1 = Token;

    //    settoken(Token)

    //    const chartdata4 = axios.post(myContext.url + 'api/CheckLogin', Token, {
    //        headers: {
    //            'Content-Type': 'application/json',
    //            'Authorization': 'Bearer ' + Token
    //        }
    //    }).then((item) => {

    //        if (item.data === null) {
    //            localStorage.clear();
    //            history("/LoginUser");
    //        }
    //        else {

    //            activestate4 = true;
    //            setchecklogin(item.data);
    //        }



    //    }).catch(() => {

    //        localStorage.clear();
    //        history("/LoginUser");

    //    });






    //}, [])

    useEffect(() => {

        /* setfund("صندوق لوتوس")*/

        const interval = setInterval(() => {
            window.location.reload(false);
        }, 300000)


        return () => clearInterval(interval)

    }, [time])



    useEffect(() => {




        setfund("صندوق لوتوس", ptime)
    }, [])






    function setfund(e, cdate) {


        if (cdate !== ptime) {
            setptime(cdate);
        }

        checkuserlogin();


        if (e !== "لطفا یک صندوق را انتخاب نمایید") {

            setfundname(e);
            setfundname1(e);

            getregnum(cdate);
            do1(e);
            do5(e, cdate);
            do6(e);
            getdata(e, cdate);
            getdata1(e, cdate);
            getorderind(e, cdate);

            getfundvalue(e, cdate);
            getbranchesdata(e, cdate);
            getbranchesdata1(e, cdate);
            getbrancheskeshavarzi(e, cdate);
           /* getbranchesdata1keshavarzi(e, cdate);*/

            getmobiledata(e, cdate)


            onlineorder(e, cdate);

            dashboardonlineorder(e, cdate);

            getmaxunith(e);
            getmaxunitho(e);

            getnavdate();
            getfundcapital(e, cdate);
            getaum();


        }



    }





    async function getbranchesdata(e, cdate) {


        branchesval = false;
        setbranch("");
        const branches = await axios.get(myContext.url + `api/GetBranches?fundname=${e}&cdate=${cdate}`);

        if (branches.data.length > 0) {

            branchesval = true;
            setbranch(branches.data);


        }

        else {
            branchesval = false;
            setbranch("");

        }



    };


    async function getbrancheskeshavarzi(e, cdate) {


        branchesvalkeshavarzi = false;
        setbranchkeshavarzi("");
        const brancheskeshavarzi = await axios.get(myContext.url + `api/GetBranchesKeshavarzi?fundname=${e}&cdate=${cdate}`);

        if (brancheskeshavarzi.data.length > 0) {

            branchesvalkeshavarzi = true;
            setbranchkeshavarzi(brancheskeshavarzi.data);


        }

        else {
            branchesvalkeshavarzi = false;
            setbranchkeshavarzi("");

        }



    };




    async function getbranchesdata1(e, cdate) {


        branchesval1 = false;
        setsubbranch("");
        const branches1 = await axios.get(myContext.url + `api/GetSubBranches?fundname=${e}&cdate=${cdate}`);

        if (branches1.data.length > 0) {

            branchesval1 = true;
            console.log(branches1)
            setsubbranch(branches1.data);


        }

        else {
            branchesval1 = false;
            setsubbranch("");

        }



    };



    //async function getbranchesdata1keshavarzi(e, cdate) {


    //    branchesval1keshavarzi = false;
    //    subsetbranchkeshavarzi("");
    //    const branches1 = await axios.get(myContext.url + `api/GetSubBranchesKeshavarzi?fundname=${e}&cdate=${cdate}`);

    //    if (branches1.data.length > 0) {

    //        branchesval1keshavarzi = true;
    //        /*console.log(branches1)*/
    //        subsetbranchkeshavarzi(branches1.data);


    //    }

    //    else {
    //        branchesval1keshavarzi = false;
    //        subsetbranchkeshavarzi("");

    //    }



    //};


    async function getmobiledata(e, cdate) {


        mobileval = false;
        setmobile("");
        const mobilebank = await axios.get(myContext.url + `api/GetMobileBank?fundname=${e}&cdate=${cdate}`);

        if (mobilebank.data.length > 0) {
            mobileval = true;
            setmobile(mobilebank.data);

        }
        else {
            mobileval = false;
            setmobile("");
        }

    };





    const myContext = useContext(AppContext);




    async function onlineorder(e, cdate) {

        setfunddata("");
        try {
            let costs = await axios.get(myContext.url + `api/OnlineOrder?fundname=${e}&cdate=${cdate}`);

            if (costs.data) {

                setfunddata(costs.data);
            }




        } catch (e) {
            setfunddata("");
        }




    }


    async function dashboardonlineorder(e, cdate) {


        try {
            onlineordercheck = false;
            setonlineorders("");
            let costs1 = await axios.get(myContext.url + `api/OnlineDashboardOrders?fundname=${e}&cdate=${cdate}`);
            if (costs1.data) {

                onlineordercheck = true;
                setonlineorders(costs1.data);

            }


        } catch (e) {
            onlineordercheck = false;
            setonlineorders("مشکل ارتباط با سرور");
        }




    }




    async function do5(e, cdate) {

        setonlinedata("");
        try {
            sodoordash = false;

            const chartdata5 = await axios.get(myContext.url + `api/OnlineOrder?fundname=${e}&cdate=${cdate}`);

            if (chartdata5.data) {
                sodoordash = true;
                setonlinedata(chartdata5.data);
            }






        } catch (e) {

            sodoordash = false;

            setonlinedata("");
        }


    }


    async function checkuserlogin() {


        let Token = localStorage.getItem("lotus_dashboard");



        const chartdata4 = await axios.post(myContext.url + 'api/CheckLogin', Token, { headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + Token } }).then((item) => {
            checkloginn = true;
            setchecklogin(() => item.data);
            localStorage.setItem(checklogin, "lotus_dashboard");
        }).catch(() => {
            checkloginn = false;
            localStorage.clear()
            history("/loginuser");

        })

    }


    async function getfundvalue(e, cdate) {

        try {

            setfundvalue("");
            chartdata7 = await axios.get(myContext.url + `api/GetNav?fundname=${e}&cdate=${cdate}`);

            if (chartdata7.data) {

                activestate7 = true;
                setfundvalue(chartdata7.data);
            }

            else {
                activestate7 = false;
                setfundvalue("");

            }





        } catch (e) {



            activestate7 = false;

            setfundvalue("");
        }


    }


    async function getfundcapital(e, cdate) {

        try {

            setfundcapital("");
            chartdata7 = await axios.get(myContext.url + `api/GetFundCapital?fundname=${e}&cdate=${cdate}`);

            if (chartdata7.data) {
                activestate7 = true;

                setfundcapital(chartdata7.data);
            }

        } catch (e) {
            activestate7 = false;
            setfundcapital("");




        }


    }


    async function do6(e) {

        try {

            setonlinedata1("");
            const chartdata6 = await axios.get(myContext.url + `api/OnlineOrder1?fundname=${e}`);

            if (chartdata6.data) {
                activestate6 = true;

                setonlinedata1(chartdata6.data);

            }

            else {
                activestate6 = false;
                setonlinedata1("");
            }


            /*setpiedata([{ name: "صدور", value: chartdata6.data[0] }, { name: " ابطال", value: chartdata6.data[1] }]);*/
        } catch (e) {

            activestate6 = false;

            setonlinedata1("");
            /* setpiedata("مشکل ارتباط با سرور");*/
        }


    }






    async function getdata(e, cdate) {

        setmostonline("");

        try {
            scrolltableloading = false;

            data = await axios.get(`${myContext.url}api/MostOnlineOrders?fundname=${e}&cdate=${cdate}`);



            if (data.data.length > 0) {
                scrolltableloading = true;

                setmostonline(data.data);
            }

        } catch (e) {

            scrolltableloading = false;

            setmostonline("");
        }




    }








    //const dateFormatter = (item) => moment(item).format("###,###");






    async function do1(e) {
        setsodoor("");

        try {
            let Parameters = [{ Name: "startdate", Value: "" }, { Name: "enddate", Value: "" }, { Name: "dsname", Value: e }];
            const chartdata = await axios.post(myContext.url + 'api/SodoorChart', Parameters)

            if (chartdata.data.length > 0) {
                activestate = true
                setsodoor(chartdata.data);

            }

        } catch (e) {

            activestate = false
            setsodoor("");
        }



    }







    async function barchartfilter() {
        setsodoor("");

        try {
            let Parameters = [{ Name: "startdate", Value: barchartstartdate }, { Name: "enddate", Value: barchartenddate }, { Name: "dsname", Value: fundname }];
            setsodoor("");
            const chartdata = await axios.post(myContext.url + 'api/SodoorChart', Parameters);
            if (chartdata.data.length > 0) {
                setsodoor(chartdata.data);

            }



        } catch (e) {
            let Parameters = [{ Name: "startdate", Value: barchartstartdate }, { Name: "enddate", Value: barchartenddate }, { Name: "dsname", Value: fundname }];
            setsodoor("");

        }




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





    async function getdata1(e, cdate) {
        setonlineordershour("")


        try {
            data1 = await axios.get(`${myContext.url}api/OnlineOrdersHourly?fundname=${e}&cdate=${cdate}`);
            if (data1.data.length > 0) {

                activestate5 = true;
                setonlineordershour(data1.data);
            }
            else {
                setonlineordershour("");
            }


        } catch (e) {

            activestate5 = true;
            setonlineordershour("");
        }



    }







    async function getregnum(cdate) {
        setregnum("")
        try {
            const registernum = await axios.get(myContext.url + `api/OnlineRegister?cdate=${cdate}`);
            if (registernum.data.length > 0) {

                registeruser = true;

                setregnum(registernum.data);
            }

            else {
                setregnum("")

            }


        } catch (e) {


            registeruser = true;

            setregnum("");
        }


    }



    async function getorderind(e, cdate) {



        setonlinedatan("");

        try {
            const ordern = await axios.get(myContext.url + `api/OnlineOrdersInd?fundname=${e}&cdate=${cdate}`);
            if (ordern.data) {
                onlinedatanloading = true;

                setonlinedatan(ordern.data);


            }
            else {
                setonlinedatan("");
            }


        } catch (e) {

            onlinedatanloading = true;

            setonlinedatan("");
        }


    }




    async function getmaxunith(e) {

        setmaxunit("");
        try {
            maxunitloading1 = false;
            const maxu = await axios.get(myContext.url + `api/MaxFundUnit1?fundname=${e}`);
            if (maxu.data.length > 0) {
                maxunitloading1 = true;
                setmaxunit(maxu.data);


            }
            else {
                setmaxunit("");

            }





        } catch (e) {

            maxunitloading = false;

            setmaxunit("");
        }




    }

    async function getmaxunitho(e) {

        setmaxunit1("");
        try {
            maxunitloading2 = false;
            const maxu = await axios.get(myContext.url + `api/MaxFundUnit2?fundname=${e}`);
            if (maxu.data.length > 0) {
                maxunitloading2 = true;
                setmaxunit1(maxu.data);


            }
            else {
                setmaxunit1("");

            }





        } catch (e) {

            maxunitloading2 = false;

            setmaxunit1("");
        }



    }


    function filterbranch(item, type) {
        try {
            let fish = item.filter(item1 => item1.rectype === type)

            if (fish.length > 0) {
                return commafy((fish[0].sodoorAmount) / 10)
            }
        } catch (e) {
            return 0
        }

       
      


    }


    function filterbranchcount(item, type) {

        try {
            let fish = item.filter(item1 => item1.rectype === type)

            if (fish.length > 0) {
                return commafy((fish[0].requestNumber))
            }

        } catch (e) {
            return 0
        }
        
       
      


    }


    function filterbranchreq(item, type) {

        try {

            let fish = item.filter(item1 => item1.rectype === type)

            if (fish.length > 0) {
                return commafy((fish[0].branchNumber))
            }
        } catch (e) {
            return 0
        }

     



    }










    async function getnavdate() {

        setnavdate(() => "");

        try {
            maxnavdate = false;
            const nav_date = await axios.get(myContext.url + `api/GetNavDate`);


            setnavdate(() => { return nav_date ? (nav_date.data) : ("مشکل ارتباط با سرور") });





            maxnavdate = true;

        } catch (e) {


            console.log("مشکل ارتباط با سرور")
            setnavdate(() => "مشکل ارتباط با سرور");
            maxnavdate = false;


        }



    }



    function showbranches(e) {

        console.log(e)
        if (e === "+") {

            setopentable(true)
        }

        else if (e === "-") {

            setopentable(false)
        }
        else {
            setopentable(true)
        }




    }


    function showbranches1(e) {

        console.log(e)
        if (e === "+") {

            setopentable1(true)
        }

        else if (e === "-") {

            setopentable1(false)
        }
        else {
            setopentable1(true)
        }




    }

    async function getaum() {

        try {
            checkaum = false;
            const aumdata = await axios.get(myContext.url + `api/AUM`);


            setAUM(() => { return aumdata ? (aumdata.data) : ("مشکل ارتباط با سرور") });





            checkaum = true;

        } catch (e) {


            console.log("مشکل ارتباط با سرور")
            setAUM(() => "مشکل ارتباط با سرور");
            checkaum = false;


        }


    }


    if (checkloginn === false) {


        return (
            <div className="d-flex justify-content-center mt-5">

                <div className="row d-flex justify-content-center">
                    <div className="col-md-6" style={{ fontSize: "18px" }}>در حال اعتبار سنجی......</div>
                    <p className="col-md-4 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} /></p>

                </div>
            </div>
        )
    }

    else if (checkloginn === true) {

        return (

            <div className="row d-flex m-0 pe-4 ps-4 vh-100" style={{ height: "auto" }}>
                <div className="col-md-12 m-0 p-0">

                    {/*<NavMenu props={checklogin} />*/}




                </div>




                <nav className="navbar navbar-expand-lg navbar-light bg-white shadow mb-2 rounded">
                    <div className="container-fluid">

                        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse justify-content-center align-items-center" id="navbarSupportedContent">
                            <ul className="d-flex navbar-nav me-auto mb-lg-0 justify-content-center align-items-center">

                                <li className="nav-item d-flex justify-content-center align-items-center">
                                    <img src="./Logo1.png" alt="لوگو" /> &nbsp; &nbsp; تاریخ گزارش گیری: &nbsp;  <span style={{ fontWeight: "bold", color: "black", fontSize: "16px" }}> {onlinedata.today}</span>
                                </li>



                                <li className="nav-item ps-5" style={{ fontSize: "14px" }}>
                                    <Dropdown as={ButtonGroup} className="col-md-6 d-inline shadow" width="100px">
                                        <Dropdown.Toggle split variant="warning" id="dropdown-basic">
                                            {fundname}
                                        </Dropdown.Toggle>

                                        <Dropdown.Menu className="super-colors" width="100px">

                                            <Dropdown.Item style={{ fontSize: "14px" }} onClick={(e) => { setfund('صندوق لوتوس', ptime) }}>صندوق لوتوس</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق پیروزان', ptime) }}>صندوق پیروزان</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق زرین', ptime) }}>صندوق زرین</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق رویان', ptime) }}>صندوق رویان</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق الزهرا', ptime) }}>صندوق الزهرا</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق طلا', ptime) }}>صندوق طلا</Dropdown.Item>
                                            <Dropdown.Item onClick={(e) => { setfund('صندوق اعتماد', ptime) }}>صندوق اعتماد</Dropdown.Item>
                                        </Dropdown.Menu>
                                    </Dropdown>


                                </li>

                                <li className="nav-item">
                                    {accessuser.includes(checklogin) ? (<a className="btn btn-warning ms-2" href="https://dashboard.lotusib.ir:8086"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(0, 60, 50)" />&nbsp; داشبورد دارایی های تامین سرمایه لوتوس پارسیان</a>) : (<li className="nav-item"></li>)}
                                </li>

                            </ul>
                            <form className="d-flex justify-content-center align-items-center rounded">

                                <Datepicker1



                                    events={[
                                        {
                                            id: 1, // it should unique
                                            title: "Test",
                                            date: ptime,

                                            style: {
                                                borderRadius: "0.25",
                                            },
                                            className: "rounded",
                                            dotColor: "#000",
                                            disabled: false,
                                            icon: "$", // also you can use component
                                        }
                                    ]}

                                    input={<input placeholder="داشبورد روزهای گذشته" className="rounded border border-dark border-2 " />}
                                    modeTheme={'white'}
                                    theme={'orange'}
                                    onChange={(val: any) => {
                                        setfund(fundname, val.format("yyyy/MM/DD"));

                                    }}
                                    className="rounded pe-2 ps-2" />


                                <NavLink tag={Link} className="text-dark" to="/"><FontAwesomeIcon icon={faUser} size="lg" className="ps-1" color="rgb(255,181,0)" /> {checklogin}  </NavLink>

                                <NavLink tag={Link} className="text-white" to="/LogOut"><FontAwesomeIcon icon={faPowerOff} size="lg" className="ps-1" color="rgb(255,181,0)" /> خروج</NavLink>

                            </form>
                        </div>
                    </div>
                </nav>


                {/*<div className="col-md-2 m-0 p-0 shadow">*/}
                {/*    <Slider />*/}


                {/*</div>*/}

                <div className="row col-12 m-0 bg-light" >


                    <div id="rightdash" className="col-lg-12 col-xl-8 rounded p-3 border border-secondary border-2 mb-2" style={{ maxWidth: "100%" }}>

                        {/*<button className="btn btn-info" onClick={() => dispatch(decrease())}>کلیک</button>*/}
                        {/*<div>count:{countsec}</div>*/}

                        <div className="row justify-content-center align-items-center g-0 mb-2 rounded shadow" >

                            <div style={{ display: "none" }}>{((onlineorders.sodooramountha + onlineorders.sodooramountho) - (onlineorders.ebtalamountha + onlineorders.ebtalamountho)) < 0 ? (setcolor = "rgb(255,230,230)") : (setcolor = "rgb(230,255,230)")}</div>
                            <div style={{ display: "none" }}>{(onlineorders.sodoorunitha - onlineorders.ebtalunitha) < 0 ? (setcolor1 = "rgb(255,230,230)") : (setcolor1 = "rgb(230,255,230)")}</div>
                            <div style={{ display: "none" }}>{(onlineorders.sodoorunitho - onlineorders.ebtalunitho) < 0 ? (setcolor2 = "rgb(255,230,230)") : (setcolor2 = "rgb(230,255,230)")}</div>
                            <div className="row col-md-12 rounded p-0">

                                <div className="col-md-12 text-center text-dark border-secondary rounded border border-dark align-items-center border-bottom-0" style={{ backgroundColor: setcolor }}>


                                    {onlineordercheck !== false ? (<div className="mb-1 text-muted p-3 " style={{ fontSize: "22px", fontWeight: "bold" }}> خالص صدور و ابطال  {fundname} : {((onlineorders.sodooramountha + onlineorders.sodooramountho) - (onlineorders.ebtalamountha + onlineorders.ebtalamountho)) < 0 ? (<div className="text-danger d-inline">{commafy(Math.round(((onlineorders.sodooramountha + onlineorders.sodooramountho) - (onlineorders.ebtalamountha + onlineorders.ebtalamountho)) / -10))}-<small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>) : (<div className="text-success d-inline">{commafy(Math.round(((onlineorders.sodooramountha + onlineorders.sodooramountho) - (onlineorders.ebtalamountha + onlineorders.ebtalamountho)) / 10))} <small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>)}</div>) : (<ReactLoading type="spinningBubbles" color="black" />)}


                                </div>


                                {onlineordercheck !== false ? (<div className="col-md-6 p-3 text-muted border border-end-0 border-dark" style={{ fontSize: "22px", fontWeight: "bold", backgroundColor: setcolor1 }}>خالص صدور و ابطال حقیقی  : {onlineorders.sodoorunitha - onlineorders.ebtalunitha < 0 ? (<div className="text-danger d-inline">{commafy(Math.round((onlineorders.sodoorunitha - onlineorders.ebtalunitha) / -10))}-<small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>) : (<div className="text-success d-inline">{commafy(Math.round((onlineorders.sodoorunitha - onlineorders.ebtalunitha) / 10))}<small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>)}</div>) : (<ReactLoading type="spinningBubbles" color="black" />)}







                                {onlineordercheck !== false ? (<div className="col-md-6 p-3 text-muted border border-dark" style={{ fontSize: "22px", fontWeight: "bold", backgroundColor: setcolor2 }}>خالص صدور و ابطال حقوقی   : {onlineorders.sodoorunitho - onlineorders.ebtalunitho < 0 ? (<div className="text-danger d-inline">{commafy(Math.round((onlineorders.sodoorunitho - onlineorders.ebtalunitho) / -10))}-<small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>) : (<div className="text-success d-inline">{commafy(Math.round((onlineorders.sodoorunitho - onlineorders.ebtalunitho) / 10))}<small style={{ fontSize: "14px" }}>&nbsp;(تومان)</small></div>)}</div>) : (<ReactLoading type="spinningBubbles" color="black" />)}




                            </div>




                        </div>

                    </div>
                    <div id="leftdash" className="col-lg-12 col-xl-4 row justify-content-start align-items-center g-0 border border-dark border-2 mb-2 rounded p-3">



                        {/*{regnum.length !== 0 ? (<div className="col-md-12 mb-1"><div className="row m-0 bg-white border border-0 border-bottom border-top"><div className="col-md-6 p-0"><div className=" bg-white text-right ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0 text-right">{commafy(regnum)}</div><div style={{ fontSize: "14px" }} className="text-muted"> تعداد ثبت نام پرتال </div></div></div><div className="d-flex justify-content-end col-md-6 bg-white align-self-center"> <span style={{ fontWeight: "bold", color: "black", fontSize: "10px" }}></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={60} width={50} />)}*/}


                        {/*{fundvalue.length !== 0 ? (<div className="col-md-12 mb-1"><div className="row m-0 bg-white border border-0 border-bottom border-top"><div className="col-md-6 p-0"><div className=" bg-white text-right ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "22px" }} className="mb-0 text-right">{commafy(Math.round(fundvalue / 10))}</div><div style={{ fontSize: "16px" }} className="text-muted text-right">دارایی تحت مدیریت </div></div></div><div className="d-flex justify-content-end col-md-6 bg-white align-self-center"> <span style={{ fontWeight: "bold", color: "black", fontSize: "10px" }}></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={60} width={50} />)}*/}

                        {regnum.length !== 0 ? (<div className="col-md-12 mb-1 border border-0 border-bottom "><div className="row m-0 justify-content-start bg-white"><div className="col-md-6 p-0"><div className="text-left ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0 border border-0 border-end"></div><div style={{ fontSize: "14px" }} className="text-muted border border-0 border-end">  تعداد ثبت نام در پرتال </div></div></div><div className="d-flex justify-content-start col-md-6 align-self-center text-left"> <span className="d-flex justify-content-start col-md-10" style={{ color: "black", fontSize: "24px" }}>{commafy(Math.round(regnum))} </span><span className="d-flex justify-content-end align-self-center col-md-2"></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={50} />)}

                        {fundvalue.length !== 0 ? (<div className="col-md-12 mb-1 border border-0 border-bottom"><div className="row m-0 justify-content-start bg-white"><div className="col-md-6 p-0"><div className="text-left ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0 border border-0 border-end"></div><div style={{ fontSize: "14px" }} className="text-muted border border-0 border-end">  AUM </div></div></div><div className="d-flex justify-content-end col-md-6 align-self-center text-left"> <span className="d-flex justify-content-start col-md-10" style={{ color: "black", fontSize: "24px" }}>{commafy(Math.round(fundvalue))} </span><span className="d-flex justify-content-end align-self-center col-md-2"></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={50} />)}
                        


                        {/*{fundvalue.length !== 0 ? (<div className="col-md-6 mb-1"><div className="row m-0 bg-white border border-0 border-bottom border-top"><div className="col-md-6 p-0"><div className=" bg-white text-right ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0">{commafy(Math.round(fundvalue / 10))}<small>(AUM)</small></div><div style={{ fontSize: "16px" }} className="text-muted"> </div></div></div><div className="d-flex justify-content-end col-md-2 align-self-center bg-white text-center"> <span style={{ fontWeight: "bold", color: "black", fontSize: "10px" }}></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={50} />)}*/}


                        {/*{fundvalue.length !== 0 ? (<div className="col-md-6" ><div className=" shadow bg-light rounded text-center border border-dark border-end" style={{ fontSize: "16px" }}>  خالص ارزش دارایی :<br /> {commafy(Math.round(fundvalue / 10))} <small className="d-inline" style={{ fontSize: "11px" }}>(تومان)</small></div></div>) : (<ReactLoading type="spinningBubbles" height={50} width={50} color="rgb(0,50,60)" />)}*/}



                        {/*{regnum.length !== 0 ? (<div className="col-md-6" ><div className=" shadow rounded text-center border border-dark border-end" id="modal1"><Maxunitmodal sandogh={fundname} maxunits={maxunit} navdate1={ navdate} customertype="حقیقی" /></div></div>) : (<ReactLoading type="spinningBubbles" height={50} width={50} color="rgb(0,50,60)" />)}*/}

                        {fundcapital.length !== 0 ? (<div className="col-md-12 mb-1 border border-0 border-bottom"><div className="row m-0 justify-content-start bg-white"><div className="col-md-6 p-0"><div className="text-left ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0 border border-0 border-end"></div><div style={{ fontSize: "14px" }} className="text-muted border border-0 border-end">  مجموع تعداد واحدهای سرمایه گذاری نزد سرمایه گذاران </div></div></div><div className="d-flex justify-content-end col-md-6 align-self-start text-left"> <span className="d-flex justify-content-start col-md-10" style={{ color: "black", fontSize: "24px" }}>{commafy(Math.round(fundcapital))} </span><span className="d-flex justify-content-end align-self-center col-md-2"></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={50} />)}


                        {AUM.length !== 0 ? (<div className="col-md-12 mb-1 border border-0 border-bottom"><div className="row m-0 justify-content-start bg-white"><div className="col-md-6 p-0"><div className="text-left ps-2" style={{ fontSize: "16px" }}><div style={{ fontSize: "24px" }} className="mb-0"></div><div style={{ fontSize: "14px" }} className="text-muted border border-0 border-end">  مجموع دارایی تحت مدیریت (همت) <a href="http://powerbi:8081/Rs/powerbi/BILinks" target="_blank"><small>جزئیات</small></a></div></div></div><div className="d-flex justify-content-end col-md-6 align-self-center text-left"> <span className="d-flex justify-content-start col-md-10" style={{ color: "black", fontSize: "24px" }}>{commafy(Math.round(AUM))} </span><span className="d-flex justify-content-end align-self-center col-md-2"></span></div></div></div>) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={50} />)}




                        {maxunit.length > 0 && maxunitloading1 !== false ? (<div className="col-md-6 border border-0 border-bottom border-end"><div className="row m-0 justify-content-start bg-white"><div className="col-md-10 p-0"><Maxunitmodal loading={maxunitloading1} sandogh={fundname} maxunits={maxunit} navdate1={navdate} customertype="حقیقی" /></div><div className="d-flex justify-content-end col-md-2 align-self-center bg-white"> <span style={{ fontWeight: "bold", color: "black", fontSize: "10px" }}></span></div></div></div>) : (JSON.stringify(maxunit))}


                        {maxunit1.length > 0 && maxunitloading2 !== false ? (<div className="col-md-6 border border-0 border-bottom"><div className="row m-0 justify-content-start bg-white"><div className="col-md-10 p-0"><Maxunitmodal loading={maxunitloading2} sandogh={fundname} maxunits={maxunit1} navdate1={navdate} customertype="حقوقی" /></div><div className="d-flex justify-content-end col-md-2 align-self-center bg-white"> <span style={{ fontWeight: "bold", color: "black", fontSize: "10px" }}></span></div></div></div>) : (JSON.stringify(maxunit))}



                    </div>



                    <div className="col-lg-12 col-xl-4 p-3 pt-0 border border-secondary border-2 rounded bg-light" style={{ maxWidth: "100%" }}>


                        <div>


                            <div className="row justify-content-center align-items-center p-3 g-0 mb-2 rounded shadow" >

                                <div className="row justify-content-center mb-1 text-dark p-3" style={{ fontSize: "20px", fontWeight: "bold" }}><div className="text-center w-50">صدور</div> </div>
                                <div className="row col-md-12 justify-content-center rounded">


                                    {/*<div className="col-md-5 pe-0 shadow text-white border border-secondary rounded" style={{ backgroundColor: "rgb(0,60,50)" }}>*/}
                                    {/*    <FontAwesomeIcon icon={faMoneyBill} size="2x" className="ps-0" color="rgb(199,201,199)" /><br />*/}

                                    {/*    {onlinedata.length !== 0 ? (<div className="pb-3" style={{ fontSize: "12px" }}>تعداد واحد ابطال: {commafy(onlinedata.ebtalamount)}</div>) : (<ReactLoading type="spinningBubbles" color="blue" color="white" />)}*/}

                                    {/*</div>*/}


                                    <div className="col-md-12 pe-1 shadow justify-content-center text-white border border-secondary rounded" style={{ backgroundColor: "rgb(0,60,50)", alignItems: "center" }}>


                                        {sodoordash !== false ? (<div className="row justify-content-center pb-3 d-inline" style={{ fontSize: "20px", fontWeight: "bold" }}><div className="text-center"> {commafy(Math.round((onlinedata.sodoorunit) / 10))} <div className="d-inline text-center" style={{ fontSize: "14px" }}>(تومان)</div></div></div>) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" color="white" /></p>)}




                                    </div>





                                </div>




                            </div>


                            <div className="row justify-content-center align-item-center g-0 border border-dark bg-light border-2" style={{ fontSize: "13px" }}>



                                <div className="col-md-12 m-0" style={{ fontSize: "13px", fontWeight: "bold", backgroundColor: "rgb(3,49,60)" }}>

                                    <div className="justify-content-center text-center" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(200,200,200)", color: "rgb(0,80,50)" }}>50 سفارش برترصدور</div>

                                    <div style={{ height: "40vh", color: "wheat" }}>
                                        <TableScrollbar>

                                            <table className="table table-responsive table-bordered bg-light shadow ">


                                                <thead>
                                                    <tr className="boder border-dark border-2 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", height: "60px", alignItems: "center" }}>
                                                        <th scope="col">ردیف</th>
                                                        <th scope="col">کدملی</th>
                                                        <th scope="col">نام مشتری</th>
                                                        <th scope="col">مبلغ صدور <small style={{ fontSize: "11px" }}>(تومان)</small> </th>
                                                        <th scope="col">نوع مشتری</th>

                                                    </tr>
                                                </thead>

                                                <tbody className="text-center border bg-light">


                                                    {mostonline.length !== 0 ? (mostonline.slice(0, 100).map(item => (
                                                        (item.orderType === "صدور") ? (


                                                            <tr>
                                                                <td >{rownum++}</td>
                                                                <td>{item.nationalCode}</td>
                                                                <td>{item.customerName}</td>
                                                                <td >{commafy(item.fundUnit)}</td>
                                                                <td>{item.customerType}</td>


                                                            </tr>
                                                        ) : (<></>)
                                                    ))) : (<tr className="text-center">
                                                        <ReactLoading type="spinningBubbles" height={100} width={"100%"} color="rgb(0,50,60)" />


                                                    </tr>

                                                    )}

                                                </tbody>


                                            </table>

                                        </TableScrollbar>
                                    </div>
                                </div>











                            </div>



                            <div className="row col-md-12 g-0 d-flex justify-content-center bg-light align-items-center mt-2 p-3 rounded shadow" style={{ fontSize: "12px" }}>
                                {/*<div className="row col-md-12 text-light p-0" style={{ fontSize: "14px", fontWeight: "bold" }}>*/}

                                {/*    <label className="col-md-6 d-inline text-light">آمار صدور به تفکیک حقیقی-حقوقی</label>*/}
                                {/*    <div className="offset-md-3 col-md-3 mb-3">*/}



                                {/*    </div>*/}
                                {/*</div>*/}


                                <div className="col-md-6 pe-2 w-90 mb-2 p-0 d-inline text-light border border-secondary text-center rounded" style={{ fontSize: "16px", backgroundColor: "rgb(0,60,50)" }} >
                                    صدور حقیقی
                                </div>
                                <div className="col-md-6 mb-2 p-0 d-inline text-light border border-secondary text-center rounded" style={{ fontSize: "16px", backgroundColor: "rgb(0,60,50)" }} >
                                    صدور حقوقی
                                </div>




                                {/*<div className="col-md-6">*/}
                                {/*    <div className="card border-secondary">*/}
                                {/*        <div className="card-header" style={{ fontSize: "12px", backgroundColor: "rgb(255,181,0)" }}>*/}
                                {/*            مجموع تعداد صدور {fundname1} ( حقیقی)*/}
                                {/*        </div>*/}
                                {/*        <div className="card-body shadow text-center" style={{ height: "50px", fontSize: "12px", backgroundColor: "rgb(255,181,0)", fontWeight: "bold" }}>*/}
                                {/*            <p className="card-text">{funddata.length !== 0 ? (commafy(onlineorders.sodooramountha)) : (0)}</p>*/}
                                {/*        </div>*/}
                                {/*    </div>*/}
                                {/*</div>*/}


                                {/*<div className="col-md-6">*/}
                                {/*    <div className="card border-secondary">*/}
                                {/*        <div className="card-header" style={{ fontSize: "12px", backgroundColor: "rgb(255,181,0)" }}>*/}
                                {/*            مجموع تعداد صدور {fundname1} ( حقوقی)*/}
                                {/*        </div>*/}
                                {/*        <div className="card-body shadow text-center" style={{ height: "50px", fontSize: "12px", backgroundColor: "rgb(255,181,0)", fontWeight: "bold" }}>*/}
                                {/*            <p className="card-text">{funddata.length !== 0 ? (commafy(onlineorders.sodooramountho)) : (0)}</p>*/}
                                {/*        </div>*/}
                                {/*    </div>*/}
                                {/*</div>*/}











                                <div className="col-md-6 text-center">
                                    <div className="card border border-secondary" style={{ fontWeight: "bold" }} >
                                        <div className="card-header text-dark" style={{ fontSize: "16px" }}>
                                            مبلغ صدور  <small style={{ fontSize: "11px" }}>(تومان)</small>
                                        </div>
                                        <div className="card-body shadow text-center" style={{ height: "50px", fontSize: "16px", backgroundColor: "rgb(255,255,255)" }}>
                                            <p className="card-text">{funddata.length !== 0 ? (commafy(onlineorders.sodoorunitha / 10)) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>)}</p>
                                        </div>
                                    </div>
                                </div>


                                <div className="col-md-6 text-center">
                                    <div className="card border border-secondary" style={{ fontWeight: "bold" }} >
                                        <div className="card-header text-dark" style={{ fontSize: "16px", backgroundColor: "rgb(255,255,255)" }}>
                                            مبلغ صدور <small style={{ fontSize: "11px" }}>(تومان)</small>
                                        </div>
                                        <div className="card-body shadow text-center" style={{ height: "50px", fontSize: "16px", backgroundColor: "rgb(255,255,255)", fontWeight: "bold" }}>
                                            <p className="card-text">{funddata.length !== 0 ? (commafy(onlineorders.sodoorunitho / 10)) : (0)}</p>
                                        </div>
                                    </div>
                                </div>




                                <div className="col-md-6 mt-2" style={{ fontSize: "16px", fontWeight: "bold" }}>

                                    <table className="table table-hover shadow text-dark border border-dark" style={{ backgroundColor: "rgb(255,255,255)" }}>





                                        {onlinedatan.sodooramounthan > 0 ? (
                                            <tbody className="text-center">
                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد تراکنش</td>

                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.sodooramounthan)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>

                                                {/*<tr style={{ height: "60px" }}>*/}

                                                {/*    <td className="text-start">متوسط مبلغ صدور  <small style={{ fontSize: "11px" }}>(تومان)</small></td>*/}

                                                {/*    <td className="text-end">{commafy(Math.round(((onlineorders.sodoorunitha) / 10) / (onlinedatan.sodooramounthan)))}</td>*/}





                                                {/*</tr>*/}





                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد صدور (نفر)</td>

                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.sodooramounthanu)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>






                                            </tbody>

                                        ) : (<tbody><tr className="text-center">
                                            اطلاعاتی یافت نشد


                                        </tr></tbody>
                                        )}





                                    </table>

                                </div>




                                <div className="col-md-6 mt-2" style={{ fontSize: "16px", fontWeight: "bold" }}>

                                    <table className="table table-hover shadow text-dark border border-dark" style={{ backgroundColor: "rgb(255,255,255)" }}>





                                        {onlinedatan.sodooramounthan > 0 ? (
                                            <tbody className="text-center">
                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد تراکنش</td>
                                                    <td className="text-center" >{onlinedatan.length !== 0 ? (commafy(onlinedatan.sodooramounthon)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>

                                                {/*<tr style={{ height: "60px" }}>*/}



                                                {/*    <td className="text-start">متوسط مبلغ صدور  <small style={{ fontSize: "11px" }}>(تومان)</small></td>*/}
                                                {/*    <td className="text-end">{commafy(Math.round(((onlineorders.sodoorunitho) / 10) / (onlinedatan.sodooramounthon)))}</td>*/}



                                                {/*</tr>*/}






                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد صدور (نفر) </td>
                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.sodooramounthonu)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>




                                            </tbody>

                                        ) : (<tbody><tr className="text-center">
                                            اطلاعاتی یافت نشد


                                        </tr></tbody>
                                        )}





                                    </table>

                                </div>

                            </div>














                        </div>
                       



                    </div>

                    <div className="col-lg-12 col-xl-4 p-3 pt-0 border bg-light border-secondary border-2 rounded" >

                        <div>


                            <div className="row justify-content-center align-items-center p-3 g-0 mb-2 rounded shadow" >

                                <div className="row justify-content-center mb-1 text-dark p-3" style={{ fontSize: "20px", fontWeight: "bold" }}><div className="text-center w-50">ابطال</div> </div>
                                <div className="row col-md-12 justify-content-center rounded">


                                    {/*<div className="col-md-5 pe-0 shadow text-white border border-secondary rounded" style={{ backgroundColor: "rgb(0,60,50)" }}>*/}
                                    {/*    <FontAwesomeIcon icon={faMoneyBill} size="2x" className="ps-0" color="rgb(199,201,199)" /><br />*/}

                                    {/*    {onlinedata.length !== 0 ? (<div className="pb-3" style={{ fontSize: "12px" }}>تعداد واحد ابطال: {commafy(onlinedata.ebtalamount)}</div>) : (<ReactLoading type="spinningBubbles" color="blue" color="white" />)}*/}

                                    {/*</div>*/}


                                    <div className="col-md-12 pe-1 shadow justify-content-center text-dark border border-secondary rounded" style={{ backgroundColor: "rgb(255,181,0)", alignItems: "center" }}>


                                        {sodoordash !== false ? (<div className="row justify-content-center pb-3 d-inline" style={{ fontSize: "20px", fontWeight: "bold" }}><div className="text-center"> {commafy(Math.round((onlinedata.ebtalunit) / 10))} <div className="d-inline text-center" style={{ fontSize: "14px" }}>(تومان)</div></div></div>) : (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" color="white" /></p>)}


                                    </div>



                                </div>


                            </div>


                            <div className="row justify-content-center align-item-center g-0 border border-dark border-2" style={{ fontSize: "13px" }}>


                                <div style={{ fontSize: "13px", fontWeight: "bold" }}>
                                    <div className=" text-dark text-center " style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(200,200,200)" }}> <p className="d-inline" style={{ color: "rgb(200,120,0)" }}>50 سفارش برترابطال</p> </div>



                                    <div style={{ height: "40vh", color: "wheat" }}>
                                        <TableScrollbar>

                                            <table className="table table-responsive table-bordered bg-light shadow ">


                                                <thead>
                                                    <tr className="boder border-dark border-2 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", fontWeight: "bold", height: "60px" }}>
                                                        <th scope="col">ردیف</th>
                                                        <th scope="col">کدملی</th>
                                                        <th scope="col">نام مشتری</th>
                                                        <th scope="col">مبلغ ابطال (تومان) </th>
                                                        <th scope="col">نوع مشتری</th>

                                                    </tr>
                                                </thead>

                                                <tbody className="text-center border bg-light">


                                                    {mostonline.length !== 0 ? (mostonline.slice(0, 100).map(item => (

                                                        (item.orderType === "ابطال") ? (
                                                            <tr>
                                                                <td >{rownum1++}</td>
                                                                <td>{item.nationalCode}</td>
                                                                <td>{item.customerName}</td>
                                                                <td>{commafy(item.fundUnit)}</td>
                                                                <td>{item.customerType}</td>


                                                            </tr>
                                                        ) : (<></>)
                                                    ))) : (<tr className="text-center">
                                                        <ReactLoading type="spinningBubbles" color="blue" height={100} width={"100%"} color="rgb(0,50,60)" />


                                                    </tr>

                                                    )}

                                                </tbody>


                                            </table>

                                        </TableScrollbar>
                                    </div>
                                </div>







                            </div>



                            <div className="row col-md-12 g-0 d-flex justify-content-center bg-light align-items-center mt-2 p-3 shadow rounded" style={{ fontSize: "12px", backgroundColor: "rgb(3,49,60)" }}>
                                {/*<div className="row col-md-12 text-light p-0" style={{ fontSize: "14px", fontWeight: "bold" }}>*/}

                                {/*    <label className="col-md-6 d-inline text-light">آمار ابطال به تفکیک حقیقی-حقوقی</label>*/}
                                {/*    <div className="offset-md-3 col-md-3 mb-3">*/}



                                {/*    </div>*/}
                                {/*</div>*/}



                                <div className="col-md-6 mb-2 p-0 d-inline text-dark border border-secondary text-center rounded" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(255,181,0)" }} >
                                    ابطال حقیقی
                                </div>
                                <div className="col-md-6 mb-2 p-0 d-inline text-dark border border-secondary text-center rounded" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(255,181,0)" }} >
                                    ابطال حقوقی
                                </div>





                                <div className="col-md-6 text-center">
                                    <div className="card border border-secondary" style={{ fontSize: "16px", backgroundColor: "rgb(255,255,255)" }}>

                                        <div className="card-header text-dark" style={{ fontWeight: "bold" }}>
                                            مبلغ ابطال  <small style={{ fontSize: "11px" }}>(تومان)</small>
                                        </div>
                                        <div className="card-body shadow text-center text-dark" style={{ height: "50px", fontSize: "16px", backgroundColor: "rgb(255,255,255)", fontWeight: "bold" }}>
                                            <p className="card-text">{funddata.length !== 0 ? (commafy((onlineorders.ebtalamountha / 10))) : (0)}</p>
                                        </div>
                                    </div>
                                </div>




                                <div className="col-md-6 text-center">
                                    <div className="card border border-secondary" style={{ fontSize: "16px", backgroundColor: "rgb(255,255,255)" }}>

                                        <div className="card-header text-dark" style={{ fontWeight: "bold" }}>
                                            مبلغ ابطال <small style={{ fontSize: "11px" }}>(تومان)</small>
                                        </div>
                                        <div className="card-body shadow text-center text-dark" style={{ height: "50px", fontSize: "16px", backgroundColor: "rgb(255,255,255)", fontWeight: "bold" }}>
                                            <p className="card-text">{funddata.length !== 0 ? (commafy((onlineorders.ebtalamountho / 10))) : (0)}</p>
                                        </div>
                                    </div>
                                </div>






                                {/*<div className="col-md-6">*/}
                                {/*    <div className="card border border-secondary">*/}
                                {/*        <div className="card-header text-light" style={{ fontSize: "12px", backgroundColor: "rgb(0,60,50)" }}>*/}
                                {/*            مجموع مبلغ ابطال {fundname1}-تومان ( حقیقی)*/}
                                {/*        </div>*/}
                                {/*        <div className="card-body shadow text-center text-light" style={{ height: "50px", fontSize: "12px", backgroundColor: "rgb(0,60,50)", fontWeight: "bold" }}>*/}
                                {/*            <p className="card-text">{funddata.length !== 0 ? (commafy((onlineorders.ebtalunitha)/10)) : (0)}</p>*/}
                                {/*        </div>*/}
                                {/*    </div>*/}
                                {/*</div>*/}


                                {/*<div className="col-md-6">*/}
                                {/*    <div className="card border border-secondary">*/}
                                {/*        <div className="card-header text-light" style={{ fontSize: "12px", backgroundColor: "rgb(0,60,50)" }}>*/}
                                {/*            مجموع مبلغ ابطال {fundname1}-تومان ( حقوقی)*/}
                                {/*        </div>*/}
                                {/*        <div className="card-body shadow text-center text-light" style={{ height: "50px", fontSize: "12px", backgroundColor: "rgb(0,60,50)", fontWeight: "bold" }}>*/}
                                {/*            <p className="card-text">{funddata.length !== 0 ? (commafy((onlineorders.ebtalunitho)/10)) : (0)}</p>*/}
                                {/*        </div>*/}
                                {/*    </div>*/}
                                {/*</div>*/}



                                <div className="col-md-6 mt-2" style={{ fontSize: "16px", fontWeight: "bold" }}>

                                    <table className="table table-hover shadow text-dark border border-dark" style={{ backgroundColor: "rgb(255,255,255)" }}>





                                        {onlinedatan.ebtalamounthan > 0 ? (
                                            <tbody className="text-center">
                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد تراکنش</td>

                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.ebtalamounthan)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>

                                                {/*<tr style={{ height: "60px" }}>*/}

                                                {/*    <td className="text-start">متوسط مبلغ صدور  <small style={{ fontSize: "11px" }}>(تومان)</small></td>*/}

                                                {/*    <td className="text-end">{commafy(Math.round(((onlineorders.ebtalunitha) / 10) / (onlinedatan.ebtalamounthan)))}</td>*/}





                                                {/*</tr>*/}





                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد ابطال (نفر)</td>

                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.ebtalamounthanu)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>


                                            </tbody>

                                        ) : (<tbody><tr className="text-center">
                                            اطلاعاتی یافت نشد


                                        </tr></tbody>
                                        )}





                                    </table>

                                </div>




                                <div className="col-md-6 mt-2" style={{ fontSize: "16px", fontWeight: "bold" }}>

                                    <table className="table table-hover shadow text-dark border border-dark" style={{ backgroundColor: "rgb(255,255,255)" }}>





                                        {onlinedatan.ebtalamounthan > 0 ? (
                                            <tbody className="text-center">
                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد تراکنش</td>
                                                    <td className="text-center" >{onlinedatan.length !== 0 ? (commafy(onlinedatan.ebtalamounthon)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>

                                                {/*<tr style={{ height: "60px" }}>*/}



                                                {/*    <td className="text-start">متوسط مبلغ صدور  <small style={{ fontSize: "11px" }}>(تومان)</small></td>*/}
                                                {/*    <td className="text-end">{commafy(Math.round(((onlineorders.ebtalunitho) / 10) / (onlinedatan.ebtalamounthon)))}</td>*/}



                                                {/*</tr>*/}






                                                <tr style={{ height: "60px" }}>

                                                    <td className="text-center">تعداد ابطال (نفر) </td>
                                                    <td className="text-center">{onlinedatan.length !== 0 ? (commafy(onlinedatan.ebtalamounthonu)) : (<ReactLoading type="spinningBubbles" color="blue" height={50} width={"100%"} color="rgb(0,50,60)" />)}</td>

                                                </tr>


                                            </tbody>

                                        ) : (<tbody><tr className="text-center">
                                            اطلاعاتی یافت نشد


                                        </tr></tbody>
                                        )}





                                    </table>

                                </div>





                                {/*<div className="row mt-2 col-md-12 justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>*/}

                                {/*    {onlinedata1.length === 0 ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>) :*/}

                                {/*        (*/}




                                {/*            <ResponsiveContainer width="100%" height={200}>*/}
                                {/*                <PieChart width={200} height={200}>*/}
                                {/*                    <Pie data={piedata} color="rgb(0,60,50)" dataKey="value" nameKey="name" cx="50%" cy="50%" fill="rgb(0,60,50)" label={renderCustomizedLabel} paddingAngle={5}*/}
                                {/*                        innerRadius={50}*/}
                                {/*                        outerRadius={70}*/}
                                {/*                    >*/}
                                {/*                        {*/}
                                {/*                            datapie.map((entry, index) => <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />)*/}
                                {/*                        }*/}


                                {/*                    </Pie>*/}
                                {/*                    <CartesianGrid stroke="rgb(0,60,50)" />*/}
                                {/*                    <Tooltip*/}
                                {/*                        wrapperStyle={{ backgroundColor: "white" }}*/}
                                {/*                        labelStyle={{ color: "rgb(0,60,50)" }}*/}
                                {/*                        itemStyle={{ color: "black" }}*/}
                                {/*                        formatter={function (value) {*/}
                                {/*                            return ` ${value.toLocaleString()}`;*/}
                                {/*                        }}*/}
                                {/*                        labelFormatter={function (value) {*/}
                                {/*                            return `نام صندوق: ${fundname}`;*/}
                                {/*                        }}*/}

                                {/*                    />*/}
                                {/*                    <Legend verticalAlign="top" height={30} />*/}
                                {/*                </PieChart>*/}
                                {/*            </ResponsiveContainer>*/}

                                {/*        )*/}


                                {/*    }*/}
                                {/*</div>*/}

                            </div>













                        </div>

                        


                    </div>



                    <div className="row col-lg-12 col-xl-4 justify-content-center rounded border border-dark bg-light border-2 g-0 p-2">

                        <div className="row col-md-12 mt-0 border-end rounded p-0 g-0">
                            <div className="col-md-12 m-0" style={{ fontSize: "15px", fontWeight: "bold" }}>

                              

                                <div className="justify-content-center  text-center" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(200,200,200)", color: "rgb(0,80,50)" }}>گزارش عملکرد شعب بانک پارسیان</div>

                                <div style={{ height: !opentable && opentable1 ? ("30vh") : opentable && !opentable1 ? ("30vh") : opentable && opentable1 ? ("35vh") : ("20vh") }}>

                                    {branchesval === true || branch.length !== 0 ? (
                                        <TableScrollbar>

                                            <table className="table table-responsive bg-light shadow">


                                                <thead>
                                                    <tr className="boder border-dark border-1 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", height: "20px", alignItems: "center" }}>
                                                        <th scope="col">منابع تجهیز شده شعب (تومان)</th>
                                                        <th scope="col">تعداد درخواست ها</th>
                                                        <th scope="col">تعداد شعب فعال</th>


                                                    </tr>
                                                </thead>

                                                <tbody className="text-center">




                                                    <tr className="text-dark" style={{ fontSize: "13px" }}>
                                                        <td className="text-start"><button className="border-0 bg-light text-secondary" value={!opentable ? ("+") : ("-")} onClick={(e) => { showbranches(e.target.value) }}>{!opentable ? ("+") : ("-")}</button>منابع تجهیز شده (اینترنتی) : &nbsp;&nbsp; {filterbranch(branch, "internet")}</td>

                                                        <td>{filterbranchcount(branch, "internet")}</td>
                                                        <td>{filterbranchreq(branch, "internet")}</td>



                                                    </tr>



                                                    {opentable ? (<tr className="text-muted" style={{ fontSize: "13px" }}>

                                                        <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp;شعبه اختصاصی لوتوس : &nbsp;&nbsp; {filterbranch(subbranch, "internet-branch")}</td>

                                                        <td className="">{filterbranchcount(subbranch, "internet-branch")}</td>
                                                        <td className="">{filterbranchreq(subbranch, "internet-branch")}</td></tr>) : (<div style={{ display: "none" }}></div>)



                                                    }

                                                    {opentable ? (<tr className="text-muted" style={{ fontSize: "13px" }}>
                                                        <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp; شعب بانک پارسیان : &nbsp;&nbsp; {filterbranch(subbranch, "internet-bank")}</td>

                                                        <td className="">{filterbranchcount(subbranch, "internet-bank")}</td>
                                                        <td className="">{filterbranchreq(subbranch, "internet-bank")}</td></tr>) : (<div style={{ display: "none" }}></div>)



                                                    }








                                                    <tr className="text-dark" style={{ fontSize: "13px" }}>
                                                        <td className="text-start"><button className="border-0 bg-light text-secondary" value={!opentable1 ? ("+") : ("-")} onClick={(e) => { showbranches1(e.target.value) }}>{!opentable1 ? ("+") : ("-")}</button>منابع تجهیز شده (فیش) : &nbsp;&nbsp; {filterbranch(branch, "fish")}</td>

                                                        <td>{filterbranchcount(branch, "fish")}</td>
                                                        <td>{filterbranchreq(branch, "fish")}</td>



                                                    </tr>



                                                    {opentable1 ? (<tr className="text-muted" style={{ fontSize: "13px" }}>
                                                        <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp;شعبه اختصاصی لوتوس : &nbsp;&nbsp; {filterbranch(subbranch, "fish-branch")}</td>

                                                        <td className="">{filterbranchcount(subbranch, "fish-branch")}</td>
                                                        <td className="">{filterbranchreq(subbranch, "fish-branch")}</td></tr>) : (<div style={{ display: "none" }}></div>)



                                                    }

                                                    {opentable1 ? (<tr className="text-muted" style={{ fontSize: "13px" }}>
                                                        <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp;شعب بانک پارسیان : &nbsp;&nbsp; {filterbranch(subbranch, "fish-bank")}</td>

                                                        <td className="">{filterbranchcount(subbranch, "fish-bank")}</td>
                                                        <td className="">{filterbranchreq(subbranch, "fish-bank")}</td></tr>) : (<div style={{ display: "none" }}></div>)



                                                    }


                                                    <tr className="border border-2 border-dark" style={{ backgroundColor: "rgb(137,141,141)" }}>
                                                        <td><FontAwesomeIcon icon={faCheck} size="0.5x" color="rgb(255, 181, 0)" />&nbsp;&nbsp;جمع کل : &nbsp;&nbsp; {filterbranch(branch, "all")}</td>
                                                        <td>{filterbranchcount(branch, "all")}</td>
                                                        <td>{filterbranchreq(branch, "all")}</td>



                                                    </tr>



                                                </tbody>





                                            </table>

                                        </TableScrollbar>

                                    ) : (<div><ReactLoading type="spinningBubbles" color="white" height={20} width={"20%"} color="rgb(255,255,255)" /></div>)}
                                </div>




                            </div>



                            <div className="col-md-12 m-0" style={{ fontSize: "15px", fontWeight: "bold" }}>

                                <div className="justify-content-center text-center" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(200,200,200)", color: "rgb(0,80,50)" }}>گزارش عملکرد شعب بانک کشاورزی</div>

                                <div style={{ height: !opentable && opentable1 ? ("25vh") : opentable && !opentable1 ? ("25vh") : opentable && opentable1 ? ("25vh") : ("12vh") }}>

                                    {branchesvalkeshavarzi === true || branchkeshavarzi.length !== 0 ? (
                                        <TableScrollbar>

                                            <table className="table table-responsive bg-light shadow">


                                                <thead>
                                                    <tr className="boder border-dark border-1 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", height: "20px", alignItems: "center" }}>
                                                        <th scope="col">منابع تجهیز شده شعب (تومان)</th>
                                                        <th scope="col">تعداد درخواست ها</th>
                                                        <th scope="col">تعداد شعب فعال</th>


                                                    </tr>
                                                </thead>

                                                <tbody className="text-center">




                                                    <tr className="text-dark" style={{ fontSize: "13px" }}>
                                                        <td className="text-start">منابع تجهیز شده (صدور اتوماتیک) : &nbsp;&nbsp; {filterbranch(branchkeshavarzi, "internet")}</td>

                                                        <td>{filterbranchcount(branchkeshavarzi, "internet")}</td>
                                                        <td>{filterbranchreq(branchkeshavarzi, "internet")}</td>



                                                    </tr>



                                                    {/*{opentable ? (<tr className="text-muted" style={{ fontSize: "13px" }}>*/}

                                                    {/*    <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp;شعبه اختصاصی لوتوس : &nbsp;&nbsp; {filterbranch(subbranchkeshavarzi, "internet-branch")}</td>*/}

                                                    {/*    <td className="">{filterbranchcount(subbranchkeshavarzi, "internet-branch")}</td>*/}
                                                    {/*    <td className="">{filterbranchreq(subbranchkeshavarzi, "internet-branch")}</td></tr>) : (<div style={{ display: "none" }}></div>)*/}



                                                    {/*}*/}

                                                    {/*{opentable ? (<tr className="text-muted" style={{ fontSize: "13px" }}>*/}
                                                    {/*    <td className="text-start ps-5"><FontAwesomeIcon icon={faCheckCircle} size="1x" color="rgb(255, 181, 0)" />&nbsp; شعب بانک کشاورزی : &nbsp;&nbsp; {filterbranch(subbranchkeshavarzi, "internet-bank")}</td>*/}

                                                    {/*    <td className="">{filterbranchcount(subbranchkeshavarzi, "internet-bank")}</td>*/}
                                                    {/*    <td className="">{filterbranchreq(subbranchkeshavarzi, "internet-bank")}</td></tr>) : (<div style={{ display: "none" }}></div>)*/}



                                                    {/*}*/}












                                                </tbody>





                                            </table>

                                        </TableScrollbar>

                                    ) : (<div><ReactLoading type="spinningBubbles" color="white" height={20} width={"20%"} color="rgb(255,255,255)" /></div>)}
                                </div>




                            </div>

                            {/*<GaugeChart id="gauge-chart5"*/}
                            {/*    nrOfLevels={20}*/}
                            {/*    colors={["rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50)", "rgb(0,60,50),rgb(255,225,0)", "rgb(255,230,0)", "rgb(255,235,0)", "rgb(255,240,0)", "rgb(255,245,0)", "rgb(255,250,0)", "rgb(255,255,0)", "rgb(255,255,50)", "rgb(255,255,100)", "rgb(255,255,150)"]}*/}

                            {/*    percent={funddata.length !== 0 ? (onlineorders.sodooramountha / (onlineorders.sodooramountho + onlineorders.sodooramountha)) : (0)}*/}
                            {/*    arcPadding={0.02}*/}
                            {/*    style={{ width: "100%", textAlign: "center" }}*/}
                            {/*    needleColor="#345243"*/}
                            {/*/>*/}



                            <div className="col-md-12 m-0 bg-light" style={{ fontSize: "15px", fontWeight: "bold" }}>

                                <div className="justify-content-center text-center" style={{ fontSize: "16px", fontWeight: "bold", backgroundColor: "rgb(200,200,200)", color: "rgb(0,80,50)" }}>گزارش عملکرد موبایل بانک و اینترنت بانک پارسیان</div>

                                <div style={{ height: "11vh", color: "wheat" }}>

                                    {branchesval === true || mobile.length !== 0 ? (
                                        <TableScrollbar>

                                            <table className="table table-responsive table-bordered bg-light shadow">


                                                <thead>
                                                    <tr className="boder border-dark border-2 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", height: "20px", alignItems: "center" }}>
                                                        <th scope="col">مجموع مبلغ صدور (تومان)</th>
                                                        <th scope="col">تعداد درخواست ها</th>



                                                    </tr>
                                                </thead>

                                                <tbody className="text-center boder border-dark border-2">



                                                    <tr>
                                                        <td >{commafy((mobile[0].sodoorAmount) / 10)}</td>
                                                        <td>{mobile[0].requestNumber}</td>




                                                    </tr>





                                                </tbody>


                                            </table>

                                        </TableScrollbar>

                                    ) : (<div><ReactLoading type="spinningBubbles" color="white" height={20} width={"20%"} color="rgb(255,255,255)" /></div>)}
                                </div>
                            </div>



                            <div className="row col-md-12 g-0" style={{ fontSize: "22px" }}>



                                <div className="col-md-12 mt-2" style={{ textAlign: "center" }}>

                                    <div className="col-md-12 m-0 rounded" style={{ backgroundColor: "rgb(250,250,250)" }}>



                                        <div className="row d-flex justify-content-around m-0" style={{ maxWidth: "100%", backgroundColor: "rgb(250,250,250)" }}>




                                            <div className="col-md-12 d-flex justify-content-center shadow border rounded" style={{ backgroundColor: "rgb(250,250,250)", fontSize: "12px" }}>





                                                بار چارت مجموع صدور و ابطال آنلاین روز جاری در ساعات مختلف کاری(کلیه اعداد بر اساس میلیارد تومان می باشند)

                                            </div>






                                            <div className="col-md-12 d-flex justify-content-center shadow border rounded" style={{ backgroundColor: "rgb(250,250,250)", fontSize: "12px", maxWidth: "100%" }}>
                                                {onlineordershour.length === 0 ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>) :

                                                    (


                                                        <ResponsiveContainer width="100%" height={230}>

                                                            <BarChart data={onlineordershour} >

                                                                <Bar name="صدور" dataKey="orderUnitSodoor" fill="rgb(0,60,50)" stroke="rgb(0,60,50)" />
                                                                <Bar name="ابطال" dataKey="orderUnitEbtal" fill="rgb(255,181,0)" stroke="rgb(255,181,0)" />
                                                                <CartesianGrid stroke="#ccc" />
                                                                <XAxis dataKey="hour" />
                                                                <Tooltip
                                                                    wrapperStyle={{ backgroundColor: "white" }}
                                                                    labelStyle={{ color: "blue" }}
                                                                    itemStyle={{ color: "blue" }}
                                                                    formatter={function (value) {
                                                                        return ` ${value.toLocaleString()}`;
                                                                    }}
                                                                    labelFormatter={function (value) {
                                                                        return `ساعت: ${value}`;
                                                                    }}
                                                                    cursor={{ fill: 'rgb(230,233,232)', opacity: '0.5' }}


                                                                />

                                                                <text x={400} y={10} fill="black" textAnchor="middle" dominantBaseline="central" >
                                                                    {/*<tspan fontSize="16" color="rgb(255,181,50)">* مجموع صدور و ابطال صندوق لوتوس به تفکیک روز *</tspan>*/}
                                                                </text>
                                                                <Legend verticalAlign="top" height={30} />
                                                                <Brush dataKey="hour" height={30} stroke="rgb(72,122,123)" />

                                                                <title />

                                                                <YAxis name="ساعت" type="number" tickFormatter={tick => { return tick.toLocaleString() }} />

                                                            </BarChart>

                                                        </ResponsiveContainer>


                                                    )


                                                }

                                            </div>


                                        </div>







                                    </div>

                                </div>

                            </div>

                            <div className="row col-md-12 g-0 p-1" style={{ fontSize: "22px" }}>
                            <div className="col-md-12" style={{ textAlign: "center" }}>
                                <div className="col-md-12 m-0 p-0 rounded" style={{ backgroundColor: "rgb(250,250,250)" }}>







                                    <div className="row d-flex justify-content-center mt-2 p-1">




                                        <div className="col-md-12 d-flex justify-content-center shadow rounded rounded" style={{ backgroundColor: "rgb(250,250,250)", fontSize: "12px" }}>





                                            لاین چارت مجموع صدور و ابطال (کلیه اعداد بر اساس میلیارد تومان می باشند)

                                        </div>






                                        <div className="col-md-12 d-flex justify-content-center shadow border m-0 rounded" style={{ backgroundColor: "rgb(250,250,250)", fontSize: "12px" }}>
                                            {activestate === false || sodoor.length === 0 ? (<p className="col-md-2 pt-2"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>) :

                                                (


                                                    <ResponsiveContainer width="100%" height={287} >

                                                        <LineChart data={sodoor} backgroundColor="rgb(137,141,141)" >

                                                            <Line name="صدور" dataKey="sumsodoor" fill="rgb(0,60,50)" stroke="rgb(0,60,50)" />
                                                            <Line name="ابطال" dataKey="sumebtal" fill="rgb(255,181,0)" stroke="rgb(255,181,0)" />
                                                            <CartesianGrid stroke="#ccc" />
                                                            <XAxis dataKey="orderdate" />
                                                            <Tooltip
                                                                wrapperStyle={{ backgroundColor: "white" }}
                                                                labelStyle={{ color: "black" }}
                                                                itemStyle={{ color: "black" }}
                                                                formatter={function (value) {
                                                                    return ` ${value.toLocaleString()}`;
                                                                }}
                                                                labelFormatter={function (value) {
                                                                    return `تاریخ: ${value}`;
                                                                }}
                                                                cursor={{ fill: 'rgb(230,233,232)', opacity: '0.5' }}


                                                            />

                                                            <text x={200} y={10} fill="black" textAnchor="middle" dominantBaseline="central" >
                                                                {/*<tspan fontSize="16" color="rgb(255,181,50)">* مجموع صدور و ابطال صندوق لوتوس به تفکیک روز *</tspan>*/}
                                                            </text>
                                                            <Legend verticalAlign="top" height={30} />
                                                            <Brush dataKey="orderdate" height={30} stroke="rgb(72,122,123)" />

                                                            <title />

                                                            <YAxis type="number" tickFormatter={tick => { return tick.toLocaleString() }} />

                                                        </LineChart>

                                                    </ResponsiveContainer>


                                                )


                                            }

                                        </div>

                                        <div className="row d-flex justify-content-start align-items-center shadow border mb-2" style={{ backgroundColor: "rgb(250,250,250)", fontSize: "12px" }}>



                                            <div className="col-md-4 mb-2">تاریخ شروع<DatePicker className="pe-2" value={barchartstartdate} onChange={(value) => { setbarchartstartdate(value) }} /></div>

                                            <div className="col-md-4 ps-2 mb-2">تاریخ پایان<DatePicker className=" ps-2" value={barchartenddate} onChange={(value) => { setbarchartenddate(value) }} /></div>

                                            <div className="col-md-4 ps-2 "><button className="col-md-3 btn btn-success ms-1" style={{ width: "auto", height: "30px", fontSize: "14px", backgroundColor: "rgb(0,60,50)" }} onClick={barchartfilter}>فیلتر</button></div>

                                        </div>
                                    </div>



                                </div>



                                </div>
                            </div>
                        </div>




                        




                        {/*<div className="col-md-12 mt-2 border-start rounded p-0 g-0" style={{ backgroundColor: "rgb(3,49,60)" }}>*/}
                        {/*    <div className="text-light mt-1 text-center" style={{ fontSize: "12px", fontWeight: "bold", backgroundColor: "rgb(3,49,60)", textAlign: "center" }}>تعداد درخواست</div>*/}
                        {/*    */}{/*<GaugeChart id="gauge-chart5"*/}
                        {/*    */}{/*    nrOfLevels={20}*/}
                        {/*    */}{/*    colors={["rgb(255,181,0)", "rgb(255,185,0)", "rgb(255,190,0)", "rgb(255,195,0)", "rgb(255,195,0)", "rgb(255,200,0)", "rgb(255,205,0)", "rgb(255,210,0)", "rgb(255,215,0)", "rgb(255,220,0),rgb(255,225,0)", "rgb(255,230,0)", "rgb(255,235,0)", "rgb(255,240,0)", "rgb(255,245,0)", "rgb(255,250,0)", "rgb(255,255,0)", "rgb(255,255,50)", "rgb(255,255,100)", "rgb(255,255,150)"]}*/}
                        {/*    */}{/*    percent={funddata.length !== 0 ? (onlineorders.ebtalamountha / (onlineorders.ebtalamountho + onlineorders.ebtalamountha)) : (0)}*/}
                        {/*    */}{/*    arcPadding={0.02}*/}
                        {/*    */}{/*    style={{ width: "100%", textAlign: "center" }}*/}
                        {/*    */}{/*/>*/}

                        {/*</div>*/}



                        {/*<div className="col-md-12 mt-2 border-start rounded p-0 g-0" style={{ backgroundColor: "rgb(3,49,60)" }}>*/}
                        {/*    <div className="text-light mt-1 text-center" style={{ fontSize: "12px", fontWeight: "bold", backgroundColor: "rgb(3,49,60)", textAlign: "center" }}>تعداد شعب فعال</div>*/}
                        {/*    */}{/*<GaugeChart id="gauge-chart5"*/}
                        {/*    */}{/*    nrOfLevels={20}*/}
                        {/*    */}{/*    colors={["rgb(255,181,0)", "rgb(255,185,0)", "rgb(255,190,0)", "rgb(255,195,0)", "rgb(255,195,0)", "rgb(255,200,0)", "rgb(255,205,0)", "rgb(255,210,0)", "rgb(255,215,0)", "rgb(255,220,0),rgb(255,225,0)", "rgb(255,230,0)", "rgb(255,235,0)", "rgb(255,240,0)", "rgb(255,245,0)", "rgb(255,250,0)", "rgb(255,255,0)", "rgb(255,255,50)", "rgb(255,255,100)", "rgb(255,255,150)"]}*/}
                        {/*    */}{/*    percent={funddata.length !== 0 ? (onlineorders.ebtalamountha / (onlineorders.ebtalamountho + onlineorders.ebtalamountha)) : (0)}*/}
                        {/*    */}{/*    arcPadding={0.02}*/}
                        {/*    */}{/*    style={{ width: "100%", textAlign: "center" }}*/}
                        {/*    */}{/*/>*/}

                        {/*</div>*/}





                    </div>





                </div>

                <Footer />




            </div >








        )


    }






}