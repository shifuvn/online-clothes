import React from "react";
import Button from "@mui/material/Button";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import PopupState, { bindTrigger, bindMenu } from "material-ui-popup-state";
import { UserManage } from "../../services/UserManage";
import { TokenManage } from "../../services/TokenManage";

const UserDropDown = () => {
  const logoutClick = () => {
    TokenManage.removeAccessToken();
    UserManage.remove();
    window.location.reload();
  };

  return (
    <PopupState variant="popover" popupId="demo-popup-menu">
      {(popupState) => (
        <React.Fragment>
          <Button
            variant="contained"
            {...bindTrigger(popupState)}
            style={{
              backgroundColor: "#f5f5f5",
              color: "#333",
              boxShadow: "none"
            }}
          >
            {UserManage.getFullName()}
          </Button>
          <Menu {...bindMenu(popupState)}>
            <MenuItem onClick={popupState.close}>Profile</MenuItem>
            <MenuItem onClick={logoutClick}>Logout</MenuItem>
          </Menu>
        </React.Fragment>
      )}
    </PopupState>
  );
};

export default UserDropDown;
