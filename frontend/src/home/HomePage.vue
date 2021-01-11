<template>
  <div>
    <img id="main" src="../../assets/concert.gif" />
    <concertSelection></concertSelection>
  </div>
</template>

<script>
import concertSelection from "./ConcertSelector";

export default {
  components: { concertSelection }
};
</script>

<style lang="css" scoped>
img#main {
  width: 100%;
}
</style>
