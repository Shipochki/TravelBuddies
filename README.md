TravelBuddies
Welcome to TravelBuddies, a comprehensive travel management application that facilitates the creation and management of user profiles, driver registrations, travel posts, group communication, and payment processing.

Table of Contents
Overview
Features
Installation
Usage
Home Page and Profile Creation
Registration and Login
User Profile Management
Becoming a Driver
Adding a Vehicle
Creating a Post
Searching for Travels
Group Management
Completing a Trip
Payment and Reviews
Additional Features
Contributing
License
Overview
TravelBuddies is an application designed to connect travelers with drivers offering travel services. Users can create profiles, register as drivers, add vehicles, post travel plans, join travel groups, and manage payments and reviews.

Features
User profile creation and management
Driver registration and verification
Vehicle addition and management
Travel post creation with detailed information
Search and join travel posts
Group communication and management
Payment processing via Stripe
Review and rating system
Real-time communication (planned feature)
Installation
To run TravelBuddies locally, follow these steps:

Clone the repository:

bash
Copy code
git clone https://github.com/yourusername/TravelBuddies.git
cd TravelBuddies
Install the required dependencies:

bash
Copy code
npm install
Set up environment variables:

Create a .env file in the root directory and add your environment variables (e.g., database credentials, Stripe API keys).
Run the application:

bash
Copy code
npm start
Usage
Home Page and Profile Creation
Home Page: The starting point of the application.
Create Profile:
Click on "Let's Go" and then "Sign Up".
Fill in personal information and upload a profile picture.
Registration and Login
Register: Complete the registration form and receive a confirmation email.
Log In: Use your credentials to log in to the application.
User Profile Management
Profile:
View user information, driver details (if applicable), and reviews.
Edit Profile:
Update profile picture and personal information.
Becoming a Driver
Register as Driver:
Click on "Become Driver" and upload driver’s license images.
Note: Personal information is not stored.
Approval Process:
Immediate driver status upon submission. Future updates will include an admin approval process.
Adding a Vehicle
Add Vehicle:
Navigate to "Add Vehicle" and enter vehicle details and image.
Edit or delete vehicles as needed.
Creating a Post
Add Post:
Fill in travel details such as destinations, date, time, price, and payment type.
If payment involves a card, a product is created in Stripe.
Searching for Travels
Search:
Enter travel criteria (e.g., locations, date range, price).
View matching posts and driver details.
Group Management
Join Group:
Join a travel group to communicate with other travelers.
Group Features:
View members, departure details, and manage messages.
Drivers can manage group members and messages.
Completing a Trip
Complete Trip:
Mark the trip as complete and trigger payment notifications.
The group remains for future communication.
Payment and Reviews
Payment:
Follow email notifications to complete payment via Stripe.
Reviews:
Leave and manage reviews for other users.
Administrators can delete reviews if necessary.
Additional Features
Statistics:
Track posts and payments.
View monthly profit statistics.
Real-Time Communication (planned):
Implement Azure SignalR Service for real-time user communication.
Contributing
Contributions are welcome! Please follow these steps to contribute:

Fork the repository.
Create a new branch for your feature or bug fix:
bash
Copy code
git checkout -b feature-name
Make your changes and commit them:
bash
Copy code
git commit -m "Description of changes"
Push to your branch:
bash
Copy code
git push origin feature-name
Create a pull request on GitHub.
License
This project is licensed under the MIT License. See the LICENSE file for details.
