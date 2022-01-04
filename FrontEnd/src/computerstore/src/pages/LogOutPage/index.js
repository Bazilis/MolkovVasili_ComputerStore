import React from "react";
import { SignOut } from "../../services/AuthorizationService";
import { Navigate } from "react-router-dom";

const LogOutPage = () => {
  // React.useEffect(() => {
  //   SignOut()
  // }, [])
  SignOut();
  return <Navigate replace to="/" />;
};

export default LogOutPage;
