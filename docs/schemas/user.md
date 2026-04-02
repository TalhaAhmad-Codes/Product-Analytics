
---

# 👤 User Schema

---

## 🛢️ Database

| Attribute | Datatype | Nullable | Constraint |
|-----------|----------|----------|------------|
| Id | Integer | `No` | Primary Key |
| Profile Picture | Byte-Array | `Yes` | --- |
| Username | String |  `No` | Unique |
| Password | String | `No` | --- |
| Role | Enum('Admin', 'Owner') | `No` | --- |
| Created At | Data Time | `No` | --- |

---

## 📋 DTOs

---

### 🔍 GET

#### Filter (Pagination)

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Username | String |  `Yes` |
| Role | Enum | `Yes` |
| Page Number | Integer | `No` |
| Page Size | Integer | `No` |

#### By Id

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |

---

### ➕ POST

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Profile Picture | Byte-Array | `Yes` |
| Username | String |  `No` |
| Password | String | `No` |
| Role | Enum | `No` |

---

### 🛠️ PATCH

#### Profile Picture

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |
| Profile Picture | Byte-Array | `Yes` |

#### Username

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |
| Username | String |  `No` |

#### Password

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |
| Old Password | String | `No` |
| New Password | String | `No` |
| Confirm Password | String | `No` |

---

### 🗑️ DELETE

| Attribute | Datatype | Nullable |
|-----------|----------|----------|
| Id | Integer | `No` |

---
