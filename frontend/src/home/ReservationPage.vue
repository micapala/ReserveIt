<template>
  <h1>
    <div>Reservation for concert {{ concertName }}</div>
    <div>Band name: {{ bandName }}</div>
    <div>Ticket price: {{ ticketPrice }} PLN</div>
    <md-button
      class="md-raised md-primary"
      :md-ripple="false"
      @click="createReservation()"
      >Confirm reservation</md-button
    >
    <md-button
      class="md-raised md-accent"
      :md-ripple="false"
      @click="goToPayment()"
      >Go to payment</md-button
    >
  </h1>
</template>

<script>
export default {
  computed: {
    concertId() {
      return this.$store.state.reservation.concert_id;
    },
    concertName() {
      return this.$store.state.reservation.concert_name;
    },
    bandName() {
      return this.$store.state.reservation.band_name;
    },
    ticketPrice() {
      return this.$store.state.reservation.ticket_price;
    },
    reservationCreated() {
      return this.$store.state.reservation.reservationCreated;
    },
  },
  methods: {
    createReservation() {
      var userName = this.$store.state.authentication.user.username;
      var concertId = this.$store.state.reservation.concert_id;

      this.$store.dispatch("reservation/reserve", { userName, concertId });
    },
    goToPayment() {
      var controlString = this.$store.state.reservation.paymentId;
      this.$store.dispatch("payment/goToPayment", { controlString });
    },
  },
};
</script>
