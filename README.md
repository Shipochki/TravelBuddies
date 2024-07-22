# TravelBuddies

Welcome to TravelBuddies, a comprehensive travel management application that facilitates the creation and management of user profiles, driver registrations, travel posts, group communication, and payment processing.

## Table of Contents

1. [Overview](#overview)
2. [Features](#features)
3. [Installation](#installation)
4. [Usage](#usage)
   - [Home Page and Profile Creation](#home-page-and-profile-creation)
   - [Registration and Login](#registration-and-login)
   - [User Profile Management](#user-profile-management)
   - [Becoming a Driver](#becoming-a-driver)
   - [Adding a Vehicle](#adding-a-vehicle)
   - [Creating a Post](#creating-a-post)
   - [Searching for Travels](#searching-for-travels)
   - [Group Management](#group-management)
   - [Completing a Trip](#completing-a-trip)
   - [Payment and Reviews](#payment-and-reviews)
5. [Additional Features](#additional-features)
6. [Contributing](#contributing)
7. [License](#license)

## Overview

TravelBuddies is an application designed to connect travelers with drivers offering travel services. Users can create profiles, register as drivers, add vehicles, post travel plans, join travel groups, and manage payments and reviews.

## Features

- User profile creation and management
- Driver registration and verification
- Vehicle addition and management
- Travel post creation with detailed information
- Search and join travel posts
- Group communication and management
- Payment processing via Stripe
- Review and rating system
- Real-time communication (planned feature)

## Installation

To run TravelBuddies locally, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/Shipochki/TravelBuddies.git
   cd TravelBuddies
2. Install the required dependencies:
  npm install
3. Set up environment variables:
   Create a .env file in the root directory and add your environment variables (e.g., database credentials, Stripe API keys).
4. Run the application:
  npm start

## Usage

### Home Page and Profile Creation

1. **Home Page**: The starting point of the application.
2. **Create Profile**: 
   - Click on "Let's Go" and then "Sign Up".
   - Fill in personal information and upload a profile picture.

### Registration and Login

1. **Register**: Complete the registration form and receive a confirmation email.
2. **Log In**: Use your credentials to log in to the application.

### User Profile Management

1. **Profile**:
   - View user information, driver details (if applicable), and reviews.
2. **Edit Profile**:
   - Update profile picture and personal information.

### Becoming a Driver

1. **Register as Driver**:
   - Click on "Become Driver" and upload driver’s license images.
   - Note: Personal information is not stored.
2. **Approval Process**:
   - Immediate driver status upon submission. Future updates will include an admin approval process.

### Adding a Vehicle

1. **Add Vehicle**:
   - Navigate to "Add Vehicle" and enter vehicle details and image.
   - Edit or delete vehicles as needed.

### Creating a Post

1. **Add Post**:
   - Click on "Post" then "Add Post".
   - Fill in travel details such as from and to destinations, description, date, time of departure, baggage or pets allowed, price per seat, available seats, payment type, and currency.
   - If payment involves a card, a product is created in Stripe.
2. **Post Management**:
   - After creation, you will see buttons to edit, complete the trip, or delete the post.
   - Deleting the post will also delete the group automatically.

### Searching for Travels

1. **Search for Trips**:
   - Open your personal account and select "Search".
   - Enter details like from and to locations (e.g., Sofia to Varna), date range, price, and baggage options.
   - Click "Search" to find matches.
2. **View Trip Details**:
   - Click on a match to see driver details, vehicle information, and reviews.
   - Join the group to navigate to the group chat.

### Group Management

1. **Join Group**:
   - Join a travel group to communicate with other travelers.
2. **Group Features**:
   - View members, departure details, and manage messages.
   - Drivers can manage group members and messages.
3. **Driver's Perspective**:
   - Drivers can kick or ban members.
   - Kicked members can rejoin; banned members cannot.
   - Drivers can delete all messages in the group as the manager.

### Completing a Trip

1. **Complete Trip**:
   - Click on "Complete Trip" to finish the trip.
   - If payment type includes card, a notification with a payment link is sent to all participants (except the driver).
   - The group remains for future travel arrangements and communication.

### Payment and Reviews

1. **Payment**:
   - Check your email for the payment notification.
   - Click on the payment link to navigate to Stripe, enter your payment details, and submit.
2. **Review System**:
   - Go to the completed group, open any member’s profile, and leave a review.
   - Only you can edit your reviews, while both you and an administrator can delete them.
   - View all reviews if they exceed three by clicking "More".

## Additional Features

- **Statistics**:
  - On the left, view stats for created posts to track each post and see who has paid.
  - The report system allows reporting issues with members.
  - The withdrawal process to transfer money from the post to your account.
- **Monthly Statistics**:
  - On the right, view monthly profit statistics.
- **Real-Time Communication** (planned):
  - Implement Azure SignalR Service for real-time user communication.
