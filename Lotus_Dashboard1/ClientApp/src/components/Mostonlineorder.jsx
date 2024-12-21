import React, { useState, useEffect } from "react";
import axios from "axios";
import { useContext } from 'react';
import AppContext from './AppContext';





export function Mostonlineorder(prob) {


    let rownum = 1;
    let data = [];
    const myContext = useContext(AppContext);
    /*let [context1, setcontext1] = useState("");*/
    
    
    console.log(prob.fundname)

    async function getdata() {

        data = await axios.get(`${myContext.url}api/MostOnlineOrders?fundname=${myContext.fundname}`)


    }


    useEffect(() => {

        
        getdata()


    },[])


    return (

        <div className="row col-md-6 justify-content-center ms-5 g-0" style={{fontSize:"12px"} }>

            

            <div className="col-md-6">

                <table className="table table-bordered table-responsive">


                    <thead>
                        <tr style={{ backgroundColor: "rgb(0,50,60)", color: "white" }}>
                            <th scope="col">ردیف</th>
                            <th scope="col">نام مشتری</th>
                            <th scope="col">تعداد واحد </th>
                            <th scope="col">نوع مشتری</th>

                        </tr>
                    </thead>

                    {/*<tbody>*/}


                    {/*    {activeunit.map(item => (*/}
                    {/*        <tr>*/}
                    {/*            <td >{rownum++}</td>*/}
                    {/*            <td>{item.fundname}</td>*/}
                    {/*            <td>{commafy(item.fundlicense)}</td>*/}



                    {/*        </tr>*/}


                    {/*    ))}*/}

                    {/*</tbody>*/}


                </table>

            </div>


            <div className="col-md-6">

                <table className="table table-bordered table-responsive">


                    <thead>
                        <tr style={{ backgroundColor: "rgb(0,50,60)", color: "white" }}>
                            <th scope="col">ردیف</th>
                            <th scope="col">نام مشتری</th>
                            <th scope="col">تعداد واحد </th>
                            <th scope="col">نوع مشتری</th>

                        </tr>
                    </thead>

                    {/*<tbody>*/}


                    {/*    {activeunit.map(item => (*/}
                    {/*        <tr>*/}
                    {/*            <td >{rownum++}</td>*/}
                    {/*            <td>{item.fundname}</td>*/}
                    {/*            <td>{commafy(item.fundlicense)}</td>*/}



                    {/*        </tr>*/}


                    {/*    ))}*/}

                    {/*</tbody>*/}


                </table>

            </div>

        </div>



    )
}