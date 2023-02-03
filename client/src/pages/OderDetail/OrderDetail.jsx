import React from "react";
import {
    MDBCard,
    MDBCardBody,
    MDBCardFooter,
    MDBCardHeader,
    MDBCardImage,
    MDBCol,
    MDBContainer,
    MDBIcon,
    MDBRow,
    MDBTypography,
} from "mdb-react-ui-kit";

export default function OrderDetails5() {
    return (
        <>
            <section className="vh-100 gradient-custom-2">
                <MDBContainer className="py-5 h-100">
                    <MDBRow className="justify-content-center align-items-center h-100">
                        <MDBCol md="10" lg="8" xl="6">
                            <MDBCard
                                className="card-stepper"
                                style={{ borderRadius: "16px" }}
                            >
                                <MDBCardHeader className="p-4">
                                    <div className="d-flex justify-content-between align-items-center">
                                        <div>
                                            <p className="text-muted mb-2">
                                                {" "}
                                                Order ID{" "}
                                                <span className="fw-bold text-body">1222528743</span>
                                            </p>
                                            <p className="text-muted mb-0">
                                                {" "}
                                                Place On{" "}
                                                <span className="fw-bold text-body">
                                                    12,March 2019
                                                </span>{" "}
                                            </p>
                                        </div>
                                        <div>
                                            <MDBTypography tag="h6" className="mb-0">
                                                {" "}
                                                <a href="#">View Details</a>{" "}
                                            </MDBTypography>
                                        </div>
                                    </div>
                                </MDBCardHeader>
                                <MDBCardBody className="p-4">
                                    <div className="d-flex flex-row mb-4 pb-2">
                                        <div className="flex-fill">
                                            <MDBTypography tag="h5" className="bold">
                                                Headphones Bose 35 II
                                            </MDBTypography>
                                            <p className="text-muted"> Qt: 1 item</p>
                                            <MDBTypography tag="h4" className="mb-3">
                                                {" "}
                                                $ 299{" "}
                                                <span className="small text-muted"> via (COD) </span>
                                            </MDBTypography>
                                            <p className="text-muted">
                                                Tracking Status on:{" "}
                                                <span className="text-body">11:30pm, Today</span>
                                            </p>
                                        </div>
                                        <div>
                                            <MDBCardImage
                                                fluid
                                                className="align-self-center"
                                                src="https://muaaothun.vn/wp-content/uploads/2022/07/ao-polo.jpg"
                                                width="250"
                                            />
                                        </div>
                                    </div>
                                    <ul
                                        id="progressbar-1"
                                        className="mx-0 mt-0 mb-5 px-0 pt-0 pb-4"
                                    >
                                        <li className="step0 active" id="step1">
                                            <span style={{ marginLeft: "22px", marginTop: "12px" }}>
                                                PLACED
                                            </span>
                                        </li>
                                        <li className="step0 active text-center" id="step2">
                                            <span>SHIPPED</span>
                                        </li>
                                        <li className="step0 text-muted text-end" id="step3">
                                            <span style={{ marginRight: "22px" }}>DELIVERED</span>
                                        </li>
                                    </ul>
                                </MDBCardBody>

                            </MDBCard>
                        </MDBCol>
                    </MDBRow>
                </MDBContainer>
            </section>
        </>
    );
}