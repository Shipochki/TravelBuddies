import "./Success.css";

export const Success = () => {
  return (
    <div className="payment-success-main">
      <div className="payment-success-container">
        <h1>Payment Successful!</h1>
        <p>Thank you for paying for your trip!</p>
        <div className="next-steps">
          <h2>What’s Next?</h2>
          <p>Your trip is complete and your payment has been received.</p>
          <p>We hope you had a wonderful experience.</p>
          <p>
            Check your email for a receipt and trip summary. If you have any
            questions or feedback,{" "}
            <a href="/support">contact our support team</a>.
          </p>
        </div>
      </div>
    </div>
  );
};
