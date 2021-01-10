<template>
  <div class="concert_selection">
    <calendar v-model="date"></calendar>
    <div id=concerts>
      <h1>Concerts that day</h1>
      <p> {{ date }} </p>
    </div>
  </div>
</template>

<script>
import calendar from "./Calendar"

export default {
  components: { calendar },
  data() {
    return {
      date: "",
    };
  },
};
</script>

<style lang="css" scoped>
.concert-selection{
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
}
.concert-selection>*first-child{
  flex: 50%;
}
div#concerts{
  flex: 50%;
  color: var(--font-color);
  text-align: center;
}
</style>
