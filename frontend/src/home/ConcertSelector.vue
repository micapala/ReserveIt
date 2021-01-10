<template>
  <div class="concert_selection">
    <calendar v-model="date"></calendar>
    <div class="concerts">
      <h1 v-if="date">Concerts on {{ date }}</h1>
      <em v-if="concerts.loading">Loading concerts...</em>
      <span v-if="concerts.error" class="text-danger">
        ERROR: {{ concerts.error }}
      </span>
      <div class="concerts_container" v-if="concerts.items">
        <concertitem
          v-for="concert in concerts.items"
          v-bind:name="concert.name"
          v-bind:price="concert.ticketPrice"
          v-bind:band_name="concert.name"
          v-bind:key="concert.id"
        ></concertitem>
      </div>
    </div>
  </div>
</template>

<script>
import calendar from "./Calendar";
import concertitem from "./concert-item";

export default {
  components: { calendar,concertitem },
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
    },
    $route(to, from) {
      this.$store.dispatch("concertsByDate/clear");
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
.concert_selection {
  display: flex;
  flex-wrap: wrap;
  margin-top: 5%;
  margin-left: 5%;
}
.concerts {
  color: var(--font-color);
  text-align: center;
  margin-left: 2rem;
}
.concerts_container {
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
  justify-content: space-between;
  margin-left: 2rem;
  margin-top: 1rem;
}
</style>
