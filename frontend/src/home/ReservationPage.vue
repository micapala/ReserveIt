<template>
  <div class="main_column">
    <div v-if="alert.message"
         :class="`alert ${alert.type}`"
    >{{ alert.message }}</div>
    <h1>Reservation for {{ concertName }}</h1>
    <h2>Band name: {{ bandName }}</h2>
    <h2>Ticket price: {{ ticketPrice }} PLN</h2>
    <button class="btn btn-primary" @click="createReservation()">
      Confirm reservation
    </button>
    <button class="btn btn-primary" @click="goToPayment()">
      Go to payment
    </button>
  </div>
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
    alert() {
      return this.$store.state.alert;
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

<style lang="css" scoped>
.alert {
  max-width: 560px;
  margin: 1rem auto;
  text-align: center;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
}
</style>
