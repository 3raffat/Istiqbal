export const setToken = (token: string) => {
  sessionStorage.setItem("access_token", token);
};
export const setRefToken = (token: string) => {
  sessionStorage.setItem("refresh_token", token);
};
export const setExpire = (Expire: string) => {
  sessionStorage.setItem("Expire", Expire);
};

export const getToken = () => {
  return sessionStorage.getItem("access_token");
};
export const getsetRefToken = () => {
  return sessionStorage.getItem("refresh_token");
};
export const getExpire = () => {
  return sessionStorage.getItem("Expire");
};

export const logout = () => {
  sessionStorage.removeItem("access_token");
  sessionStorage.removeItem("refresh_token");
  sessionStorage.removeItem("Expire");
  clearRole();
};

export const isAuthenticated = () => !!getToken();

export const setRole = (role: string) => sessionStorage.setItem("role", role);
export const getRole = () => sessionStorage.getItem("role");
export const clearRole = () => sessionStorage.removeItem("role");

export const isAdmin = () => getRole() === "ROLE_ADMIN";

function base64UrlDecode(str: string): string | null {
  if (!str) return null;
  try {
    // Base64URL → Base64
    str = str.replace(/-/g, "+").replace(/_/g, "/");
    while (str.length % 4) str += "=";

    if (typeof (globalThis as any).Buffer !== "undefined") {
      return (globalThis as any).Buffer.from(str, "base64").toString("utf8");
    }

    if (typeof atob !== "undefined") {
      return decodeURIComponent(
        atob(str)
          .split("")
          .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
          .join("")
      );
    }

    return null;
  } catch {
    return null;
  }
}

// Safe decode JWT payload
export function decodeJwtSafe(token: string | null): any | null {
  if (!token) return null;
  const parts = token.split(".");
  if (parts.length !== 3) return null;

  const payloadStr = base64UrlDecode(parts[1]);
  if (!payloadStr) return null;

  try {
    const payload = JSON.parse(payloadStr);
    return payload;
  } catch {
    return null;
  }
}
