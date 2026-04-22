# 🚨 Fraud Monitoring & Risk Analytics System (.NET)

A real-time fraud detection and transaction monitoring system built with **ASP.NET Core Web API, Entity Framework Core, and a JavaScript analytics dashboard**.

This project simulates a **banking-grade transaction monitoring engine**, similar to what you would find in corporate banking or fintech fraud detection platforms.

---

## 📌 Overview

Modern banking systems process thousands of transactions per second. Detecting fraudulent behaviour requires:

- Rule-based risk evaluation
- Real-time alert generation
- Account-level risk aggregation
- Behavioural pattern detection

This system implements a simplified version of that pipeline.

---

## 🎯 Core Objectives

- Detect potentially fraudulent transactions in real time
- Apply configurable fraud detection rules
- Assign risk scores and risk levels
- Generate alerts per transaction
- Provide a dashboard for monitoring risk exposure
- Aggregate risk at account level

---

## 🧠 Fraud Detection Logic

The system uses a **modular rule engine** to evaluate transactions.

### ⚙️ Implemented Rules

#### 1. Large Transaction Rule
- Flags transactions above a threshold (e.g. 50,000)
- High-value transactions increase risk score

#### 2. Velocity Rule
- Detects multiple transactions within a short time window (10 minutes)
- Simulates rapid fraud attempts

#### 3. Location Risk Rule
- Flags transactions originating from non-local regions
- Simulates geo-anomalies in banking activity

---

## 📊 Risk Scoring Model

Each rule contributes a weighted score:

| Rule | Score Impact |
|------|-------------|
| Large Transaction | +50 |
| Velocity Spike | +40 |
| Foreign Location | +30 |

### Risk Classification

- **Low Risk**: 0–49  
- **Medium Risk**: 50–99  
- **High Risk**: 100+

---

## 🏗️ Architecture

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- MySQL Database
- Rule Engine (custom implementation)

### Frontend
- HTML/CSS/JavaScript
- Chart.js for analytics
- Real-time API consumption

---

## 🧩 Key Features

### 🔹 Transaction Processing
- Stores financial transactions
- Links each transaction to an account

### 🔹 Fraud Rule Engine
- Extensible rule-based system
- Easily add/remove rules

### 🔹 Alert System
- Generates alerts when risk thresholds are exceeded
- Stores rule triggers per transaction

### 🔹 Account Risk Aggregation
- Calculates total risk per account
- Identifies high-risk customers

### 🔹 Analytics Dashboard
- Risk distribution (pie chart)
- Alerts over time (line chart)
- Account-level risk monitoring
- Filtering by account and risk level

---

## 📈 Dashboard Features

- 📊 Risk distribution visualization
- 📉 Time-based alert trends
- 🧾 Transaction alert table
- 🚨 High-risk account summary
- 🔎 Account filtering
- ⚡ Real-time refresh capability

---

## 🧪 Example Fraud Scenario

### Input Transactions

| Account | Amount | Location |
|--------|--------|----------|
| ACC1 | 100,000 | SA |
| ACC1 | 5,000 | UK |
| ACC2 | 80,000 | SA |

### Output

- High-risk alerts triggered
- Account ACC1 flagged
- Risk score escalated
- Dashboard reflects anomaly patterns

---

## 🛠️ Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- MySQL

### Frontend
- HTML5
- CSS3
- JavaScript (ES6)
- Chart.js

---

## 🧠 Key Design Patterns Used

- Service Layer Architecture
- Rule Engine Pattern
- Dependency Injection
- Repository-like abstraction via DbContext
- DTO-based API responses

---

## 🚀 How to Run the Project

### 1. Clone Repository
```bash
git clone https://github.com/your-username/fraud-monitoring-system.git