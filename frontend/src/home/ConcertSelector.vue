<template>
  <div class="concert_selection">
    <calendar v-model="date"></calendar>
    <div id="concerts">
      <h1>Concerts that day</h1>
      <p>elo {{ date }}</p>
      <em v-if="concerts.loading">Loading concerts...</em>
      <span v-if="concerts.error" class="text-danger"
        >ERROR: {{ concerts.error }}</span
      >
      <ul v-if="concerts.items">
        <li v-for="concert in concerts.items" :key="concert.id">
          {{ concert.TicketPrice }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import calendar from "./Calendar";

export default {
  components: { calendar },
  data: () => ({
    date: ""
  }),
  computed: {
    concerts() {
      return this.$store.state.concertsByDate.byDate;
    },
  },
  watch: {
    date: function() {
      this.handleDateSelect();
    }
  },
  methods: {
    handleDateSelect() {
      const { date } = this;
      this.$store.dispatch("concertsByDate/getByDate", { date });
    }
  }
};
</script>

<style lang="css" scoped>
.concert-selection {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
}
.concert-selection > *first-child {
  flex: 50%;
}
div#concerts {
  flex: 50%;
  color: var(--font-color);
  text-align: center;
}
</style>
