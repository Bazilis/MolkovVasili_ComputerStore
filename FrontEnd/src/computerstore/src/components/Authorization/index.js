import React from "react";
import { Navigate } from "react-router-dom";

function parseJwt(token) {
  if (!token) {
    return null;
  }
  var base64Url = token.split(".")[1];
  var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
  var jsonPayload = decodeURIComponent(
    atob(base64)
      .split("")
      .map(function (c) {
        return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join("")
  );

  return JSON.parse(jsonPayload);
}

const AuthorizationChecker = (props) => {
  const [shouldSignOut, setShouldSignOut] = React.useState(false);

  React.useEffect(() => {
    const token = window.localStorage.getItem("token");
    const tokenData = parseJwt(token);
    const expDate = new Date(tokenData.exp * 1000);
    const timerId = setInterval(() => {
      console.log(expDate, new Date());
      setShouldSignOut(expDate < new Date());
    }, 1000);
    return () => {
      clearInterval(timerId);
    };
  }, []);

  return (
    <>
      {shouldSignOut && <Navigate replace to="/logout" />}
      {!shouldSignOut && props.children}
    </>
  );
};

export const Authorization = (props) => (WrappedComponent) => {
  return (
    <AuthorizationChecker>
      <WrappedComponent {...props} />
    </AuthorizationChecker>
  );
};
