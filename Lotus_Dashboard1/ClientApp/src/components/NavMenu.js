import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faPowerOff, faFaceLaugh, faTable } from '@fortawesome/free-solid-svg-icons';




export function NavMenu(props) {


    return (


        <header style={{ height: "50px" }} className="backgruondblack shadow border-bottom rounded pe-4 ps-4">
            <Navbar style={{ height: "50px" }} className="navbar-expand-sm text-white navbar-toggleable-sm ng-white box-shadow mb-0" light>

                <NavbarBrand tag={Link} to="https://lotusib.ir">{/*<img style={{ textAlign: "right", height: "50px", width: "100px" }} src="./lotusbaner.jpg"></img>*/}</NavbarBrand>
                <NavbarBrand tag={Link} to="/" style={{ fontSize: "16px" }} className="text-white m-0 p-0">سامانه داشبورد شرکت لوتوس پارسیان</NavbarBrand>
                <NavbarToggler />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={true} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-white" to="/"><FontAwesomeIcon icon={faUser} size="lg" className="ps-1" color="rgb(255,181,0)" /> {props.props}  <FontAwesomeIcon icon={faFaceLaugh} size="lg" className="ps-1"  /></NavLink>
                            
                        </NavItem>


                        <NavItem>
                         
                            <NavLink tag={Link} className="text-white" to="/LogOut"><FontAwesomeIcon icon={faPowerOff} size="lg" className="ps-1" color="rgb(255,181,0)" /> خروج</NavLink>
                        </NavItem>




                    </ul>
                </Collapse>

            </Navbar>
        </header>





    );

}
