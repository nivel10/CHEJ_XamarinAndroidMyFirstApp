namespace CHEJ_AndroidMyFirstApp
{
    using Android.App;
    using Android.Graphics;
    using Android.OS;
    using Android.Runtime;
    using Android.Support.V7.App;
    using Android.Views;
    using Android.Widget;
    using Resources.Models;
    using System;

    [Activity(
        Label = "@string/app_name",
        Theme = "@style/AppTheme",
        MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region Attributes

        private TextView lblTitle;
        private EditText txtNuber1;
        private EditText txtNuber2;
        private EditText txtResult;
        private TextView lblMessages;
        private Button btnSum;
        private Button btnSub;
        private Button btnMul;
        private Button btnDiv;

        #endregion Attributes

        #region Activity States

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Toast.MakeText(this, "OnCreate", ToastLength.Short).Show();
            //  The activity is created

            this.GetToast();

            this.InitialData();
        }

        //protected override void OnStart()
        //{
        //    base.OnStart();
        //    Toast.MakeText(this, "OnStart", ToastLength.Short).Show();
        //    //  The activity is about to become visible
        //}

        //protected override void OnResume()
        //{
        //    base.OnResume();
        //    Toast.MakeText(this, "OsResume", ToastLength.Short).Show();
        //    //  The activity has become visible (now is "resume")
        //}

        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    Toast.MakeText(this, "OnPause", ToastLength.Short).Show();
        //}

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    Toast.MakeText(this, "OnStop", ToastLength.Short).Show();
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //    Toast.MakeText(this, "OnDestroy", ToastLength.Short).Show();
        //}

        #endregion Activity States

        #region Methods

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitialData()
        {
            this.lblTitle = (TextView)FindViewById(Resource.Id.lblTitle);
            this.txtNuber1 = (EditText)FindViewById(Resource.Id.txtNumber1);
            this.txtNuber2 = (EditText)FindViewById(Resource.Id.txtNumber2);
            this.txtResult = (EditText)FindViewById(Resource.Id.txtResult);
            this.txtResult.Enabled = false;
            this.lblMessages = (TextView)FindViewById(Resource.Id.lblMessage);

            this.btnSum = (Button)FindViewById(Resource.Id.cmdSum);
            this.btnSub = (Button)FindViewById(Resource.Id.cmdSub);
            this.btnMul = (Button)FindViewById(Resource.Id.cmdMul);
            this.btnDiv = (Button)FindViewById(Resource.Id.cmdDiv);

            this.SetButtonFuntion();
        }

        private void SetButtonFuntion()
        {
            this.btnSum.Click += delegate
            {
                this.SetOperationAdd(null, null);
            };

            this.btnSub.Click += delegate
            {
                this.SetOperationSub(null, null);
            };

            this.btnMul.Click += delegate
            {
                this.SetOperationMul(null, null);
            };

            this.btnDiv.Click += delegate
            {
                this.SetOperationDiv(null, null);
            };
        }

        public void SetOperationAdd(object sender, EventArgs e)
        {
            this.SetOperation(0);
        }

        private void SetOperationSub(object sender, EventArgs e)
        {
            this.SetOperation(1);
        }

        private void SetOperationMul(object sender, EventArgs e)
        {
            this.SetOperation(2);
        }

        private void SetOperationDiv(object sender, EventArgs e)
        {
            this.SetOperation(3);
        }

        private void SetOperation(int _operationType)
        {
            var response = this.GetResult(
                double.Parse(
                    string.IsNullOrEmpty(this.txtNuber1.Text) ? "0" : this.txtNuber1.Text),
                double.Parse(
                    string.IsNullOrEmpty(this.txtNuber2.Text) ? "0" : this.txtNuber2.Text),
                _operationType);

            this.txtResult.Text = response.Result.ToString();

            this.lblMessages.SetTextColor(
                response.IsSucced ? Color.Green : Color.Red);
            this.lblMessages.Text = response.Message;
        }

        private Response GetResult(
            double _number1,
            double _number2,
            int _operationType)
        {
            try
            {
                switch (_operationType)
                {
                    case 0:
                        return new Response
                        {
                            IsSucced = true,
                            Message = "Method add is ok...!!!",
                            Result = Math.Round((_number1 + _number2), 2),
                        };

                    case 1:
                        return new Response
                        {
                            IsSucced = true,
                            Message = "Method substract is ok...!!!",
                            Result = Math.Round((_number1 - _number2), 2),
                        };

                    case 2:
                        return new Response
                        {
                            IsSucced = true,
                            Message = "Method multiply is ok...!!!",
                            Result = Math.Round((_number1 * _number2), 2),
                        };

                    case 3:

                        if (_number2 == 0)
                        {
                            return new Response
                            {
                                IsSucced = false,
                                Message = "Error cannot be divided by zero...!!!",
                                Result = 0,
                            };
                        }

                        return new Response
                        {
                            IsSucced = true,
                            Message = "Method divide is ok...!!!",
                            Result = Math.Round((_number1 / _number2), 2),
                        };
                }

                return new Response
                {
                    IsSucced = true,
                    Message = "No operation wass chosen...!!!",
                    Result = 0,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        #region Old Code

        private void GetToast()
        {
            var response = this.AverageCal(
                5.35,
                7.87,
                9.09);
            if (!response.IsSucced)
            {
                Toast.MakeText(this, response.Message, ToastLength.Short).Show();
                return;
            }

            response = this.IsApproved((double)response.Result);

            Toast.MakeText(this, response.Message, ToastLength.Short).Show();
        }

        private Response AverageCal(
           double _maths,
           double _chemistry,
           double _physical)
        {
            try
            {
                return new Response
                {
                    IsSucced = true,
                    Message = "Method is ok...!!!",
                    Result = Math.Round((_maths + _chemistry + _physical) / 3.00, 2),
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        private Response IsApproved(
            double _average)
        {
            try
            {
                if (_average >= 6)
                {
                    return new Response
                    {
                        IsSucced = true,
                        Message = "Approved",
                    };
                }
                return new Response
                {
                    IsSucced = true,
                    Message = "Reprobate",
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSucced = false,
                    Message = ex.Message,
                };
            }
        }

        #endregion Old Code

        #endregion Methods
    }
}