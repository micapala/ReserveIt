<template>
  <div class="concert_item">
    <div class="concert_header">
      <div class="concert_name">{{ name }}</div>
    </div>
    <div class="concert_body">
      <div class="band_name">Playing {{ band_name }}</div>
      <div class="ticket_price">Ticket price: {{ price }}$</div>
    </div>
    <div class="concert_footer">
      <button @click="reserve(id, name, band_name, price)">Reserve</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "concert_item",
  props: ["id", "name", "price", "band_name"],
  methods: {
    reserve(concert_id, concert_name, band_name, ticket_price) {
      this.$store.dispatch("reservation/moveToReservation", {
        concert_id,
        concert_name,
        band_name,
        ticket_price,
      });
    },
  },
};
</script>

<style lang="css" scoped>
.concert_item {
  margin-top: 2rem;
  width: 70%;
  top: 100%;
  box-shadow: 0px 14px 45px rgba(0, 0, 0, 0.25),
    0px 10px 18px rgba(0, 0, 0, 0.22);
  background: var(--background-color);
}
.concert_header {
  padding: 15px 10px;
  background: var(--primary-color);
  color: var(--font-color);
  text-transform: uppercase;
}
.concert_name {
  font-size: 1.2rem;
  line-height: 1.5rem;
}
.ticket_price {
  font-size: 1rem;
  line-height: 1.5rem;
}
.concert_footer {
  text-align: right;
}
.concert_footer button {
  padding: 8px 10px;
  text-transform: uppercase;
  font-weight: bold;
  color: var(--primary-color);
  opacity: 0.9;
  background: transparent;
  outline: none;
  border: 0;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}
.concert_footer button:hover {
  opacity: 1;
}
</style>
