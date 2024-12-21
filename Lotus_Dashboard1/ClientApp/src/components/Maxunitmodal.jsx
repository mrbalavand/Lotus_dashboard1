import React, { useState } from 'react';

import Modal from 'react-modal';
import TableScrollbar from 'react-table-scrollbar';
import ReactLoading from 'react-loading';
import './Onlinedash.css';


const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        color: "rgb(255,181,0)",
    },
};




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


// Make sure to bind modal to your appElement (https://reactcommunity.org/react-modal/accessibility/)
/*Modal.setAppElement('#yourAppElement');*/

export function Maxunitmodal(props1) {
    let subtitle;
    const [modalIsOpen, setIsOpen] = useState(false);

    function openModal() {
        setIsOpen(true);
    }

    function afterOpenModal() {
        // references are now sync'd and can be accessed.
        subtitle.style.color = "black";
    }

    function closeModal() {
        setIsOpen(false);
    }

    let rownum = 1;

    return (

        <div>
        {props1.loading !== false ? (<div className="bg-white">
                <button className="bg-white border-0 text-start" onClick={openModal} id="modal3"><small style={{fontSize:"13px"}} className="d-inline bg-white text-muted text-start">50 {props1.customertype === "حقیقی" ? ("نفر") : ("شرکت")} برتر {props1.sandogh} با بیشترین دارایی ({props1.customertype}) تا تاریخ {props1.navdate1}</small></button>
                <Modal
                    isOpen={modalIsOpen}
                    onAfterOpen={afterOpenModal}
                    onRequestClose={closeModal}
                    style={customStyles}
                    contentLabel="Example Modal"
                >

                    <button onClick={closeModal} type="button" className="btn-close" aria-label="Close"></button>
                    <small className="d-inline" style={{ fontSize: "14px", fontFamily: "IranSans", color: "black", fontWeight: "bold" }}>50 {props1.customertype === "حقیقی" ? ("نفر") : ("شرکت")} برتر {props1.sandogh} با بیشترین دارایی ({props1.customertype}) تا تاریخ {props1.navdate1}</small>
                    <div style={{ height: "88vh", color: "black" }} >
                        <TableScrollbar>

                            <table className="table table-responsive table-bordered bg-light shadow " style={{ fontFamily: "IranSans", fontSize: "14px" }}>


                                <thead>
                                    <tr className="boder border-dark border-2 text-dark text-center" style={{ backgroundColor: "rgb(137,141,141)", height: "60px", alignItems: "center" }}>
                                        <th scope="col">ردیف</th>

                                        <th scope="col">نام مشتری</th>
                                        <th scope="col">کدملی</th>
                                        <th scope="col">تعداد واحد </th>


                                    </tr>
                                </thead>

                                <tbody className="text-center border border-dark">

                                    {console.log(props1) }
                                    {props1.maxunits.length> 0 ? (props1.maxunits.map(item => (



                                        <tr>
                                            <td >{rownum++}</td>
                                            <td>{item.customerName1}</td>
                                            <td>{item.nationalCode}</td>

                                            <td>{commafy(item.fundUnit)}</td>



                                        </tr>

                                    ))) : (<tr className="text-center">
                                        <ReactLoading type="spinningBubbles" height={100} width={"100%"} color="rgb(0,50,60)" />


                                    </tr>

                                    )}

                                </tbody>


                            </table>

                        </TableScrollbar>
                    </div>
                </Modal>

                
            </div>):(<div>خطا در دریافت اطلاعات</div>)}
      </div>
    );
}

