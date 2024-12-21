import React, { useState, useEffect } from "react";

import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';

export function CountTime(props) {

    const [time, setTime] = useState(props.time);
    const [time1, setTime1] = useState(new Date());



    useEffect(() => {

        /* setfund("صندوق لوتوس")*/

        const interval = setInterval(() => {
            setTime((time) => time - 1)
            /*setfund("صندوق لوتوس")*/
        }, 1000)


        return () => clearInterval(interval)

    }, [])




    useEffect(() => {

        setTime1(new Date());



    }, []);




    return (

        <div className="row">


            {/*{time} ثانیه تا آپدیت اطلاعات  &nbsp; &nbsp;*/}
            <NavLink style={{ fontSize: "14px" }} className="text-white m-0 ps-5 pe-5"> زمان آخرین آپدیت داشبورد: {time1.toLocaleTimeString()}</NavLink>
            

        </div>


    )
}