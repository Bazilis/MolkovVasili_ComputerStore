import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import UserInfo from "./pages/UserInfo";
import LoginPage from "./pages/LoginPage";
import { Authorization } from "./components/Authorization";
import LogOutPage from "./pages/LogOutPage";

const Router = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route exact path={"/"} element={<HomePage />} />{" "}
        <Route
          exact
          path="/user"
          element={(props) => Authorization(props)(UserInfo)}
        ></Route>
        <Route exact path="/login" element={LoginPage} />
        <Route exact path="/logout" element={LogOutPage} />
      </Routes>
    </BrowserRouter>
  );
};

export default Router;
