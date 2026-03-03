# Mortgage Risk Cal (.NET DLL)

This project contains a custom .NET Class Library used for mortgage application risk evaluation.

## Features
- DTI Calculation
- Income threshold validation
- Loan limit validation
- Risk flag evaluation
- Auto approve / manual review decision logic

## Technologies Used
- .NET Framework 4.8
- C#
- Designed for Kofax TotalAgility Integration

## How It Works
The DLL exposes a MortgageRiskEngine class which evaluates:
- Monthly Income
- Existing Debt
- Loan Amount
- Customer Risk Flag

It returns:
- Approval Status
- DTI Ratio
- Decision Reason
