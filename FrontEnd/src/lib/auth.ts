"use client"

export interface AdminUser {
  username: string
  name: string
}

// Mock admin credentials - في التطبيق الحقيقي، يجب استخدام قاعدة بيانات
const ADMIN_CREDENTIALS = {
  username: "admin",
  password: "admin123",
}

export function login(username: string, password: string): boolean {
  if (username === ADMIN_CREDENTIALS.username && password === ADMIN_CREDENTIALS.password) {
    const user: AdminUser = {
      username: ADMIN_CREDENTIALS.username,
      name: "مدير النظام",
    }
    localStorage.setItem("admin_user", JSON.stringify(user))
    return true
  }
  return false
}

export function logout(): void {
  localStorage.removeItem("admin_user")
}

export function getAdminUser(): AdminUser | null {
  if (typeof window === "undefined") return null
  const userStr = localStorage.getItem("admin_user")
  if (!userStr) return null
  try {
    return JSON.parse(userStr)
  } catch {
    return null
  }
}

export function isAuthenticated(): boolean {
  return getAdminUser() !== null
}
