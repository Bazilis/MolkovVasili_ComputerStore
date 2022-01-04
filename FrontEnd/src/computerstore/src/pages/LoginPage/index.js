import React from "react";
import { SignIn } from "../../services/AuthorizationService";

const LoginPage = () => {
  const [login, setLogin] = React.useState("string");
  const [password, setPassword] = React.useState("string");

  const onSubmit = (e) => {
    e.preventDefault();
    const isLoginProvided = login && login !== "";
    const isPasswordProvided = password && password !== "";
    if (isLoginProvided && isPasswordProvided) {
      SignIn(login, password).then((result) => {
        window.location = "/user";
      });
    }
  };

  const onLoginChanged = (e) => {
    setLogin(e.target.value);
  };

  const onPasswordChanged = (e) => {
    setPassword(e.target.value);
  };

  return (
    <div>
      <form onSubmit={onSubmit}>
        <input
          id="login"
          name="login"
          value={login}
          onChange={onLoginChanged}
          placeholder="login"
        />
        <br />
        <input
          id="password"
          name="password"
          value={password}
          onChange={onPasswordChanged}
          placeholder="password"
        />
        <button type="submit">Submit</button>
      </form>
    </div>
  );
};

export default LoginPage;
