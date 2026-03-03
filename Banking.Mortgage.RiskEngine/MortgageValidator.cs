using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Banking.Mortgage.RiskEngine
{
    public class MortgageDecision
    {
        public bool IsApproved { get; set; }
        public decimal DTIRatio { get; set; }
        public string DecisionReason { get; set; }
    }

    public class MortgageValidator
    {
        private const decimal MaxDTI = 0.45m;        // 45%
        private const decimal MinMonthlyIncome = 3000m;
        private const decimal MaxLoanAmount = 1000000m;

        public MortgageDecision EvaluateApplication(
            decimal monthlyIncome,
            decimal existingDebt,
            decimal requestedLoanAmount,
            bool isHighRiskCustomer)
        {
            var decision = new MortgageDecision();

            try
            {
                if (monthlyIncome < MinMonthlyIncome)
                {
                    decision.IsApproved = false;
                    decision.DecisionReason = "Income below minimum eligibility.";
                    return decision;
                }

                if (requestedLoanAmount > MaxLoanAmount)
                {
                    decision.IsApproved = false;
                    decision.DecisionReason = "Requested loan exceeds policy limit.";
                    return decision;
                }

                decimal dti = (existingDebt + requestedLoanAmount) / monthlyIncome;
                decision.DTIRatio = Math.Round(dti, 2);

                if (isHighRiskCustomer)
                {
                    decision.IsApproved = false;
                    decision.DecisionReason = "Customer marked as high risk.";
                    return decision;
                }

                if (dti > MaxDTI)
                {
                    decision.IsApproved = false;
                    decision.DecisionReason = "DTI ratio exceeds allowed threshold.";
                    return decision;
                }

                decision.IsApproved = true;
                decision.DecisionReason = "Application auto-approved.";
                return decision;
            }
            catch (Exception ex)
            {
                decision.IsApproved = false;
                decision.DecisionReason = "System validation error: " + ex.Message;
                return decision;
            }
        }
    }
}
