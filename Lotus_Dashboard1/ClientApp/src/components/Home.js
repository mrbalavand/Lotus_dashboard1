import React, { useState, useEffect } from "react";
import { Slider } from "./Slider";
import { NavMenu } from "../components/NavMenu";
import { Selectfunds } from "../components/Selectfunds";
import { Footer } from "../components/Footer";
import axios from "axios";
import "../../src/Highcharts.css";
/*import Highcharts from "highcharts";*/
import { BarChart, Bar, CartesianGrid, XAxis, YAxis, Legend, Tooltip, ResponsiveContainer, Brush } from 'recharts';
import { Line, LineChart } from 'recharts';
import { Pie, PieChart, Cell } from 'recharts';
import { Treemap, Label } from 'recharts';
import Skeleton from 'react-loading-skeleton';
import ReactLoading from 'react-loading';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import AppContext from './AppContext';
import './DatePicker.css';

import { DatePicker } from 'react-persian-datepicker';
/*import FusionCharts from "fusioncharts";*/
/*import charts from "fusioncharts/fusioncharts.charts";*/
/*import ReactFusioncharts from "react-fusioncharts";*/
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMoneyBill, faCommentDollar, faDonate, faCreditCard, faLineChart, faBalanceScale } from '@fortawesome/free-solid-svg-icons';
import { FundTable } from "../components/FundTable";



//charts(FusionCharts);



let activestate = false;
let activestate1 = false;
let activestate2 = false;
let activestate3 = false;
let activestate4 = false;
let activestate5 = false;
let COLORS = ['#003C32', '#FFB500', '#00685E', '#6FA287', '#487A7B'];
/*window.Highcharts = Highcharts;*/
let labels = [];
let threebar = [];
let labels1 = [];
let threebar1= [];

/*let item1 = document.getElementsByClassName('raphael-group-vMfEcfuz');*/






export function Home() {


    let tokendata = [{ token1: "" }];
    let [sodoor, setsodoor] = useState([]);

    let [fundlicense, setfundlicense] = useState([]);

    let [register, setregister] = useState([]);

    let [maxunit, setmaxunit] = useState([]);
    let [maxunit1, setmaxunit1] = useState([]);
    let [maxunit2, setmaxunit2] = useState([]);
    let [checklogin, setchecklogin] = useState([]);
    let [token, settoken] = useState([]);
    let history = useNavigate();
    let [linechartstartdate, setlinechartstartdate] = useState("");
    let [linechartenddate, setlinechartenddate] = useState("");
    let [barchartstartdate, setbarchartstartdate] = useState("");
    let [barchartenddate, setbarchartenddate] = useState("");






    const myContext = useContext(AppContext);


    //const dateFormatter = (item) => moment(item).format("###,###");
    useEffect(() => {

        let Token = localStorage.getItem("lotus_dashboard");


        tokendata.token1 = Token;

        settoken(Token)

        const chartdata4 = axios.post(myContext.url + 'api/CheckLogin', Token, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + Token
            }
        }).then((item) => {
            activestate4 = true;
            setchecklogin(item.data);


        }).catch(() => {

            goHome()

        });






    }, [])



    useEffect(() => {





        do1()








    }, [])

    async function do1() {
        let Parameters = [{ Name: "startdate", Value: "" }, { Name: "enddate", Value: "" }];
        const chartdata = await axios.post(myContext.url + 'api/SodoorChart', Parameters)
        activestate = true
        setsodoor(chartdata.data);

    }


    useEffect(() => {


        do2()

    }, [])


    async function do2() {

        const chartdata1 = await axios.get(myContext.url + 'api/FundLicense')
        activestate1 = true
        setfundlicense(chartdata1.data);
    }


    useEffect(() => {






        do3()


    }, []);



    async function do3() {

        let Parameters = [{ Name: "startdate", Value: "" }, { Name: "enddate", Value: "" }];
        const chartdata2 = await axios.post(myContext.url + 'api/RegisterChart', Parameters);
        activestate2 = true;
        setregister(chartdata2.data);
    }

    let i1 = 0;
    useEffect(() => {


        do4()
        do5()

    }, [])


    async function do4() {


        const chartdata3 = await axios.get(myContext.url + 'api/MaxFundUnit');
        activestate3 = true;
        setmaxunit(chartdata3.data);

        chartdata3.data.map((items) => {
            labels.push(items.customerName1);
            threebar.push({ label: items.customerName, value: items.fundUnit });

            i1 = i1 + 1;
        })
        setmaxunit1(threebar)

    }

    async function do5() {


        const chartdata10 = await axios.get(myContext.url + 'api/MaxFundUnit1');
        activestate3 = true;
        setmaxunit2(chartdata10.data);

        chartdata10.data.map((items) => {
            labels1.push(items.customerName1);
            threebar1.push({ label: items.customerName, value: items.fundUnit });

            i1 = i1 + 1;
        })
        /*setmaxunit1(threebar)*/

    }



    let dataSource = {
        chart: {
            caption: "نمودار بارچارت سه بعدی 10 نفر برتر صندوق لوتوس از لحاظ تعداد واحد",
            yFormatNumberScale: "1",
            baseFontSize: "13",
            numberScaleValue: "1000,1000,1000",
            numberScaleUnit: "هزار واحد,میلیون واحد,میلیارد واحد",
            thousandSeparator: ".",

            decimalSeparator: ",",

            baseFont: "IranSans",
            xaxisname: "نام مشتری به همراه کد ملی",
            yaxisname: "تعداد واحد",
            decimals: "3",

            theme: "fusion"
        },
        data: maxunit1.slice(0, 10)
    };








    async function linechartfilter() {


        let Parameters = [{ Name: "startdate", Value: linechartstartdate }, { Name: "enddate", Value: linechartenddate }];
        setregister("");
        const chartdata2 = await axios.post(myContext.url + 'api/RegisterChart', Parameters).then((item) => {


            /*activestate2 = false;*/
            setregister(item.data);


        });







    }






    async function barchartfilter() {


        let Parameters = [{ Name: "startdate", Value: barchartstartdate }, { Name: "enddate", Value: barchartenddate }];
        setsodoor("");
        const chartdata = await axios.post(myContext.url + 'api/SodoorChart', Parameters).then((item) => {
            /*activestate = true*/
            setsodoor(item.data);

        });



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

    const goHome = () => {
        history("/LoginUser");
    };


    const DEFAULT_COLORS = ['#003C32', '#FFB500', '#00685E', '#6FA287', '#487A7B', '#6f5137', '#54585A', "#898D8D", "#9f7d23", "#C7C9C7"];

    const CustomizedContent = (props) => {
        const { depth, x, y, width, height, index, name } = props;

        return (
            <g>
                <rect
                    x={x}
                    y={y}
                    width={width}
                    height={height}
                    style={{
                        fill:
                            depth < 2
                                ? DEFAULT_COLORS[index % DEFAULT_COLORS.length]
                                : 'none',
                        stroke: '#fff',
                        strokeWidth: 2 / (depth + 1e-10),
                        strokeOpacity: 1 / (depth + 1e-10),
                    }}
                />
                {depth === 1 ? (
                    <text
                        x={x + width / 2}
                        y={y + height / 2 + 7}
                        textAnchor="middle"
                        fill="#fff"
                        fontSize={14}
                    >
                        {name}
                    </text>
                ) : null}
                {depth === 1 ? (
                    <text
                        x={x + 120}
                        y={y + 18}
                        fill="#fff"
                        fontSize={12}
                        fillOpacity={0.9}
                    >
                        {labels1[index]}
                    </text>
                ) : null}
            </g>
        );
    };




    const renderCustomizedLabel = ({
        x, y, name
    }) => {
        return (
            <text x={x + 30} y={y} fill="rgb(199,201,199)" textAnchor="end" dominantBaseline="central">
                {name}
            </text>
        );
    };

    if (checklogin.length === 0) {


        return (
            <div className="d-flex justify-content-center mt-5">

                <div className="row d-flex justify-content-center">
                    <div className="col-md-6" style={{ fontSize: "18px" }} className="text-danger">در حال اعتبار سنجی......</div>
                    <p className="col-md-4 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>

                </div>
            </div>
        )
    }

    else {

        return (

            <div className="row d-flex m-0 pe-4 ps-4">
                <div className="col-md-12 m-0 p-0 shadow">

                    <NavMenu props={checklogin} />
                </div>


                <div className="col-md-2 m-0 p-0 shadow">
                    <Slider />


                </div>

                <div className="col-md-10 m-0 p-2 pt-4 shadow" style={{ backgroundColor: "rgb(199,201,199)" }}>



                    <Selectfunds />

                    <div className="d-flex justify-content-center shadow mt-5">
                        <div className="col-md-12 me-5 ms-5 shadow" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>
                            <label className="col-md-12 text-center mb-2">نمودار درختی 10 حقوقی برتر لوتوس به غیر از تامین سرمایه</label>
                            {activestate3 === false ? (<p><Skeleton count={1} width={700} height={300} color="white" /></p>) :

                                (


                                    <ResponsiveContainer width="100%" height={400} >

                                        <Treemap data={maxunit2} backgroundColor="rgb(137,141,141)" dataKey="fundUnit" nameKey="customerName" content={<CustomizedContent />}>

                                            <CartesianGrid stroke="#ccc" />
                                            <XAxis dataKey="cusomerName" />
                                            <Tooltip
                                                wrapperStyle={{ backgroundColor: "white" }}
                                                labelStyle={{ color: "black" }}
                                                itemStyle={{ color: "black" }}
                                                formatter={function (value) {
                                                    return `تعداد واحد: ${value.toLocaleString()}`;
                                                }}
                                                labelFormatter={function (value) {
                                                    return `${value}`;
                                                }}

                                            />

                                            <text x={700} y={10} fill="black" textAnchor="middle" dominantBaseline="central" >
                                                <tspan fontSize="16" color="rgb(255,181,50)">  </tspan>
                                            </text>
                                            <Legend verticalAlign="top" height={30} />

                                            <Label />


                                            <YAxis type="number" tickFormatter={tick => { return tick.toLocaleString() }} />


                                        </Treemap>
                                    </ResponsiveContainer>


                                )


                            }
                        </div>
                    </div>

                    <FundTable />

                    


                    <div className="row d-flex justify-content-around m-0 pe-5 ps-5 pt-5">
                        {/*<div className="col-md-12 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>*/}

                        {/*    نمودار درختی 10 نفر برتر صندوق لوتوس از لحاظ تعداد واحد*/}

                        {/*</div>*/}
                    </div>

                    <div className="row d-flex justify-content-center m-0 pe-5 ps-5 m-0 pt-0 rounded-5" style={{ width: "auto", height: "500px", borderRadius: "10%" }}>
                        {/*{activestate3 === false || maxunit1.length === 0 ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="white" /></p>) : (*/}
                        {/*    <ReactFusioncharts*/}
                        {/*        type="column3d"*/}
                        {/*        width="100%"*/}
                        {/*        height="100%"*/}
                        {/*        dataFormat="JSON"*/}
                        {/*        dataSource={dataSource}*/}
                        {/*    />*/}
                        {/*)}*/}
                        {/*{console.log(dataSource)}*/}






                    </div>



                    <div className="row d-flex justify-content-around m-0 pe-0 ps-0 pt-5">


                        <div className="col-md-5 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>


                            لاین چارت تعداد ثبت نامی ها صندوق لوتوس


                        </div>

                        <div className="col-md-5 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>
                            پای چارت تعداد واحد فعال در صندوقهای سرمایه گذاری





                        </div>





                        <div className="col-md-5 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>


                            {register.length === 0 || activestate2 === false ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="rgb(0,50,60)" height={100} width={"50%"} /></p>) :

                                (


                                    <ResponsiveContainer width="100%" height={400}>

                                        <LineChart data={register} backgroundColor="rgb(137,141,141)">

                                            <Line name="تعداد ثبت نامی صندوق لوتوس" dataKey="registercount" fill="rgb(0,60,50)" />

                                            <CartesianGrid stroke="#ccc" />
                                            <XAxis dataKey="registerdate" />
                                            <Tooltip
                                                wrapperStyle={{ backgroundColor: "white" }}
                                                labelStyle={{ color: "black" }}
                                                itemStyle={{ color: "black" }}
                                                formatter={function (value) {
                                                    return ` ${value}`;
                                                }}
                                                labelFormatter={function (value) {
                                                    return `تاریخ: ${value}`;
                                                }}

                                            />

                                            {/*<text x={400} y={10} fill="black" textAnchor="middle" dominantBaseline="central" >*/}
                                            {/*    <tspan fontSize="16" color="rgb(255,181,50)">* مجموع صدور و ابطال صندوق لوتوس به تفکیک روز *</tspan>*/}
                                            {/*</text>*/}
                                            <Legend verticalAlign="top" height={30} />
                                            <Brush dataKey="registerdate" height={30} stroke="rgb(72,122,123)" />

                                            <title />

                                            <YAxis type="number" tickFormatter={tick => { return tick.toLocaleString() }} />

                                        </LineChart>

                                    </ResponsiveContainer>


                                )}





                        </div>
                        {console.log(fundlicense) }
                        <div className="col-md-5 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>

                            {activestate1 === false ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>) :

                                (




                                    <ResponsiveContainer width="100%" height={400}>
                                        <PieChart width={730} height={300}>
                                            <Pie data={fundlicense} color="rgb(0,60,50)" dataKey="fundlicense" nameKey="fundname" cx="50%" cy="50%" fill="rgb(0,60,50)" label={renderCustomizedLabel} paddingAngle={5}
                                                innerRadius={130}
                                                outerRadius={150}
                                            >
                                                {
                                                    fundlicense.map((entry, index) => <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />)
                                                }


                                            </Pie>
                                            <CartesianGrid stroke="rgb(0,60,50)" />
                                            <Tooltip
                                                wrapperStyle={{ backgroundColor: "white" }}
                                                labelStyle={{ color: "rgb(0,60,50)" }}
                                                itemStyle={{ color: "black" }}
                                                formatter={function (value) {
                                                    return `تعداد افراد دارای واحدهای فعال: ${value.toLocaleString()}`;
                                                }}
                                                labelFormatter={function (value) {
                                                    return `نام صندوق: ${value}`;
                                                }}

                                            />
                                            <Legend verticalAlign="top" height={30} />
                                        </PieChart>
                                    </ResponsiveContainer>

                                )


                            }
                        </div>


                        <div className="col-md-5 d-flex justify-content-start align-items-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>


                            <p className="mb-0 pe-2">تاریخ شروع</p><DatePicker value={linechartstartdate} onChange={(value) => { setlinechartstartdate(value) }} />

                            <p className="mb-0 pe-2 ps-2">تاریخ پایان</p><DatePicker value={linechartenddate} onChange={(value) => { setlinechartenddate(value) }} />

                            <button className=" btn btn-success ms-3" style={{ width: "50px", height: "30px", fontSize: "14px", backgroundColor: "rgb(0,60,50)" }} onClick={linechartfilter}>فیلتر</button>

                        </div>


                        <div className="col-md-5 d-flex justify-content-around align-items-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>






                        </div>


                    </div>




                    <div className="row d-flex justify-content-around m-0 pe-5 ps-5 pt-5">




                        <div className="col-md-12 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>





                            بار چارت مجموع صدور و ابطال صندوق لوتوس (کلیه اعداد بر اساس میلیارد ریال می باشند)

                        </div>






                        <div className="col-md-12 d-flex justify-content-center shadow border" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>
                            {activestate === false || sodoor.length === 0 ? (<p className="col-md-2 pt-5"><ReactLoading type="spinningBubbles" color="blue" height={100} width={"50%"} color="rgb(0,50,60)" /></p>) :

                                (


                                    <ResponsiveContainer width="100%" height={400}>

                                        <BarChart data={sodoor} backgroundColor="rgb(137,141,141)">

                                            <Bar name="صدور" dataKey="sumsodoor" fill="rgb(0,60,50)" />
                                            <Bar name="ابطال" dataKey="sumebtal" fill="rgb(255,181,50)" />
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

                                            {/*<text x={400} y={10} fill="black" textAnchor="middle" dominantBaseline="central" >*/}
                                            {/*    <tspan fontSize="16" color="rgb(255,181,50)">* مجموع صدور و ابطال صندوق لوتوس به تفکیک روز *</tspan>*/}
                                            {/*</text>*/}
                                            <Legend verticalAlign="top" height={30} />
                                            <Brush dataKey="orderdate" height={30} stroke="rgb(72,122,123)" />

                                            <title />

                                            <YAxis type="number" tickFormatter={tick => { return tick.toLocaleString() }} />

                                        </BarChart>

                                    </ResponsiveContainer>


                                )


                            }

                        </div>

                        <div className="col-md-12 d-flex justify-content-start align-items-center shadow border mb-0" style={{ borderRadius: "25px", backgroundColor: "rgb(250,250,250)" }}>



                            <p className="mb-0 pe-2">تاریخ شروع</p><DatePicker value={barchartstartdate} onChange={(value) => { setbarchartstartdate(value) }} />

                            <p className="mb-0 pe-2 ps-2">تاریخ پایان</p><DatePicker value={barchartenddate} onChange={(value) => { setbarchartenddate(value) }} />

                            <button className=" btn btn-success ms-3" style={{ width: "50px", height: "30px", fontSize: "14px", backgroundColor: "rgb(0,60,50)" }} onClick={barchartfilter}>فیلتر</button>

                        </div>
                    </div>



                </div>

                <Footer />
            </div>








        )


    }






}