const getAllCustomers = () => new Promise((resolve, reject) => {
  resolve ([
    {
      id:1,
      name:"nathan",
      birthday: "5/27/1986",
      favoriteBarber: "Jimbo",
      notes: "High and Tight"
    },
    {
      id: 2
    }
  ])
})

  export default getAllCustomers;
