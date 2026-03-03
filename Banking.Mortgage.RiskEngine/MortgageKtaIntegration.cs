using System;

namespace Banking.Mortgage.RiskEngine
{
    public class MortgageKtaIntegration
    {
        public void ExecuteRiskEvaluation(dynamic Document)
        {
            // Convert field values
            decimal income = Convert.ToDecimal(Document.Fields["MonthlyIncome"].Value);
            decimal debt = Convert.ToDecimal(Document.Fields["ExistingDebt"].Value);
            decimal loan = Convert.ToDecimal(Document.Fields["LoanAmount"].Value);
            bool riskFlag = Document.Fields["RiskFlag"].Value == "Y";

            // Call validator DLL
            MortgageValidator validator = new MortgageValidator();
            MortgageDecision result = validator.EvaluateApplication(income, debt, loan, riskFlag);

            // Populate back to document
            Document.Fields["DTIRatio"].Value = result.DTIRatio.ToString();
            Document.Fields["DecisionReason"].Value = result.DecisionReason;

            // Add validation error if not approved
            if (!result.IsApproved)
            {
                Document.ValidationErrors.Add("Application requires manual underwriting review.");
            }
        }
    }
}