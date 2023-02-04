const CURRENT_USER_KEY = "current_user";

const setCurrentUser = (user) => {
  localStorage.setItem(CURRENT_USER_KEY, JSON.stringify(user));
};

const getCurrentUser = () => {
  const user = localStorage.getItem(CURRENT_USER_KEY);
  return JSON.parse(user);
};

const getFullName = () => {
  const user = getCurrentUser();
  return user?.fullName ?? "NoName";
};

const remove = () => {
  localStorage.removeItem(CURRENT_USER_KEY);
};

export const UserManage = {
  setCurrentUser,
  getCurrentUser,
  getFullName,
  remove
};
