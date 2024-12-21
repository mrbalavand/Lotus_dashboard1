import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faPowerOff, faFaceLaugh, faTable } from '@fortawesome/free-solid-svg-icons';




export function Footer() {


    return (


        <footer style={{ height: "50px" }} className="bg-white shadow border-bottom rounded d-flex justify-content-center">
            <Navbar style={{ height: "50px" }} className="navbar-expand-sm text-dark navbar-toggleable-sm ng-white box-shadow mb-0" light>

                
                <NavbarBrand tag={Link} to="/" style={{ fontSize: "12px" }} className="text-dark m-0 p-0 text-center d-block">حق نشر برای تامین سرمایه لوتوس پارسیان محفوظ است.</NavbarBrand>
                <NavbarToggler />
              

            </Navbar>
        </footer>





    );

}
